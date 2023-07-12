using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 属性
    public float runSpeed = 3;

    public float jumpSpeed = 6;

    public float climbSpeed = 4;

    public short maxJumpTimes = 1;

    public short currentJumpTimes = 0;

    public bool alwaysClimbEnabled = false;

    public bool alwaysJumpEnabled = false;

    public short coinAmount = 0;

    private bool attackEnabled = true;

    private float originalGravityScale = 0;

    // 组件
    private Rigidbody2D ownRigidbody2D;

    private Animator ownAnimator;

    private BoxCollider2D feetBoxCollider2D;

    private EntityController ownEntityController;

    private Collider2D attackChildCollider2D;

    private bool IsRunning => Mathf.Abs(ownRigidbody2D.velocity.x) > Mathf.Epsilon;

    private bool IsFalling => ownRigidbody2D.velocity.y < -Mathf.Epsilon;

    private bool IsOnPlatform => IsOnGround || IsOnOneWayPlatform || IsOnLadder || IsOnItem;

    private bool IsOnGround => feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));

    private bool IsOnOneWayPlatform => feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

    private bool IsOnLadder => feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"));

    private bool IsOnItem => feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Item"));

    // Start is called before the first frame update
    private void Start()
    {
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();
        feetBoxCollider2D = GetComponent<BoxCollider2D>();
        ownEntityController = GetComponent<EntityController>();
        attackChildCollider2D = GetComponentInChildren<PolygonCollider2D>();
        ownEntityController.RegisterOnInjured((damage) =>
        {
            ownAnimator.SetTrigger("Injured");
            GameController.Instance.FlashScreen();
        });
        originalGravityScale = ownRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchAnimation();
        MoveHandler();
        JumpHandler();
        DownOneWayPlatformHandler();
        ClimbLadderHandler();
        AttackHandler();
    }

    private void ClimbLadderHandler()
    {
        if (IsOnLadder || alwaysClimbEnabled)
        {
            float climbDirection = Input.GetAxis("Vertical");
            if (Input.GetButton("Jump")) climbDirection = 1;
            if (Input.GetButton("Downstairs")) climbDirection = -1;

            if (Mathf.Abs(climbDirection) > 0.5f)
            {
                ownAnimator.SetBool("Climbing", true);
                ownRigidbody2D.gravityScale = 0;
                ownRigidbody2D.velocity = new Vector2(ownRigidbody2D.velocity.x, climbDirection * climbSpeed);
            }
            else
            {
                if (ownAnimator.GetBool("Climbing"))
                {
                    ownRigidbody2D.velocity = new Vector2(ownRigidbody2D.velocity.x, 0);
                }
                ownAnimator.SetBool("Climbing", false);
            }
        }
        else
        {
            ownAnimator.SetBool("Climbing", false);
            ownRigidbody2D.gravityScale = originalGravityScale;
        }
    }

    private void FlipHandler()
    {
        if (IsRunning)
        {
            if (ownRigidbody2D.velocity.x > Mathf.Epsilon)
            {
                // 向右
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (ownRigidbody2D.velocity.x < -Mathf.Epsilon)
            {
                // 向左
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void MoveHandler()
    {
        // handler velocity
        float moveDirection = Input.GetAxis("Horizontal");

        if (ownEntityController.IsDead) moveDirection = 0;

        ownRigidbody2D.velocity = new Vector2(moveDirection * runSpeed, ownRigidbody2D.velocity.y);
        // handle animation
        ownAnimator.SetBool("Running", IsRunning);
        // flip sprite
        FlipHandler();
    }

    private void JumpHandler()
    {
        if (ownEntityController.IsDead) return;

        if (ownAnimator.GetBool("Idle"))
        {
            currentJumpTimes = 0;
        }

        // 爬梯子时不能跳跃
        if (Input.GetButtonDown("Jump") && !IsOnLadder)
        {
            if (currentJumpTimes < maxJumpTimes || alwaysJumpEnabled)
            {
                ownAnimator.SetBool("Jumping", true);
                ownRigidbody2D.velocity = new Vector2(ownRigidbody2D.velocity.x, jumpSpeed);
                currentJumpTimes++;
            }
        }
    }

    private void AttackHandler()
    {
        if (ownEntityController.IsDead) return;
        if (Input.GetButtonDown("Attack") && attackEnabled)
        {
            attackEnabled = false;
            ownAnimator.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }

        IEnumerator DisableHitBox()
        {
            yield return new WaitForSeconds(0.03f);
            attackChildCollider2D.enabled = false;
            StartCoroutine(EnableAttack());
        }

        IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(0.33f);
            attackChildCollider2D.enabled = true;
            StartCoroutine(DisableHitBox());
        }

        IEnumerator EnableAttack()
        {
            yield return new WaitForSeconds(ownEntityController.attackInterval);
            attackEnabled = true;
        }
    }

    private void DownOneWayPlatformHandler()
    {
        if (Input.GetButton("Downstairs") && !ownEntityController.IsDead)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), false);
        }
    }

    private void SwitchAnimation()
    {
        ownAnimator.SetBool("Idle", IsOnPlatform && !ownAnimator.GetBool("Jumping"));

        ownAnimator.SetBool("Falling", IsFalling);
        if (IsFalling)
        {
            ownAnimator.SetBool("Jumping", false);
        }

        if (IsOnPlatform && !IsOnLadder)
        {
            ownAnimator.SetBool("Falling", false);
        }

        ownAnimator.SetBool("Dead", ownEntityController.IsDead);
        if (ownAnimator.GetBool("Climbing"))
        {
            ownAnimator.SetBool("Jumping", false);
        }
    }
}