using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector2 initSpeed;

    // 设置得大一点以防一生成就爆炸了
    public float explodeCountdown = 100;

    public float deleteTime = 1.2f;

    public GameObject explosionRange;

    private float timer;

    private Rigidbody2D ownRigidbody;

    private Animator ownAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        ownRigidbody = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();

        StartCoroutine(DelayInitialize());
    }

    private IEnumerator DelayInitialize()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        AudioControllerHelpers.PlayBombFuse();
        ownRigidbody.velocity = transform.right * initSpeed.x + transform.up * initSpeed.y;
    }

    // Update is called once per frame
    private void Update()
    {
        CountdownHandler();
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        // 爆炸动画
        if (timer >= explodeCountdown)
        {
            // 恢复正常速度
            ownAnimator.speed = 1;
            ownAnimator.SetTrigger("Explode");
        }
        else
        {
            // 否则设置闪烁动画播放速度加快
            ownAnimator.speed = Mathf.Lerp(1, 10, timer / explodeCountdown);
        }
    }

    // 由帧事件调用
    private void ExplodeHandler()
    {
        AudioControllerHelpers.PlayBombExplode();
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    // 由帧事件调用
    private void DeleteBomb()
    {
        Destroy(gameObject);
    }

    private void CountdownHandler()
    {
        timer += Time.deltaTime;
    }
}