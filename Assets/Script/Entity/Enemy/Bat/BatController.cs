using Cinemachine;
using System.Collections;
using UnityEngine;

public class BatController : MonoBehaviour
{
    // ÊôÐÔ
    public float moveSpeed = 2.0f;

    public float moveWaitTime = 5.0f;

    public float damage = 1;

    public float attackInterval = 0.5f;

    public Transform leftDownPositionLimit;

    public Transform rightUpPositionLimit;

    public Transform nextPosition;

    public TrophyController trophyController;

    public Range<float> batChripRange;

    public bool isStatic = false;

    private float chripInterval;

    private float waitedTime = 0;

    private float lastAttackTime = -100;

    // ×é¼þ
    private EntityController ownEntityController;

    private CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    private void Start()
    {
        ownEntityController = GetComponent<EntityController>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        ownEntityController.RegisterOnDeath(() =>
        {
            GenerateFallingItem();
            AudioControllerHelpers.PlayBatDeath();
            Destroy(gameObject);
        });

        ownEntityController.RegisterOnInjured((damage) =>
        {
            CameraShack();
            AudioControllerHelpers.PlayRandomBatHurt();
        });
        nextPosition.position = GenerateRandomPosition();

        chripInterval = Random.Range(batChripRange.min, batChripRange.max);
        StartCoroutine(Chrip());
    }

    private IEnumerator Chrip()
    {
        yield return new WaitForSeconds(chripInterval);
        AudioControllerHelpers.PlayRandomBatIdle();
        chripInterval = Random.Range(batChripRange.min, batChripRange.max);
    }

    private void GenerateFallingItem()
    {
        if (trophyController == null) return;
        trophyController.GenerateTrophies(gameObject);
    }

    private void CameraShack()
    {
        impulseSource.GenerateImpulse();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHandler();
    }

    private void MoveHandler()
    {
        if (isStatic) return;
        transform.position = Vector2.MoveTowards(transform.position, nextPosition.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPosition.position) < Mathf.Epsilon)
        {
            if (waitedTime >= moveWaitTime)
            {
                nextPosition.position = GenerateRandomPosition();
                AudioControllerHelpers.PlayBatFly();
                waitedTime = 0;
            }
            else
            {
                waitedTime += Time.deltaTime;
            }
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(
                Random.Range(leftDownPositionLimit.position.x, rightUpPositionLimit.position.x),
                Random.Range(leftDownPositionLimit.position.y, rightUpPositionLimit.position.y)
            );
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.collider.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            if (Time.time - lastAttackTime <= attackInterval) return;
            lastAttackTime = Time.time;

            EntityController otherController = other.gameObject.GetComponent<EntityController>();
            if (otherController != null)
            {
                otherController.TakeDamege(damage);
            }
        }
    }
}