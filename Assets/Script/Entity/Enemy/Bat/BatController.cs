using Cinemachine;
using UnityEngine;

public class BatController : MonoBehaviour
{
    // ����
    public float moveSpeed = 2.0f;

    public float moveWaitTime = 5.0f;

    public float damage = 1;

    public float attackInterval = 0.5f;

    public Transform leftDownPositionLimit;

    public Transform rightUpPositionLimit;

    public Transform nextPosition;

    public TrophyController trophyController;

    private float waitedTime = 0;

    private float lastAttackTime = -100;

    // ���
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
            Destroy(gameObject);
        });

        ownEntityController.RegisterOnInjured((damage) => { CameraShack(); });
        nextPosition.position = GenerateRandomPosition();
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
        transform.position = Vector2.MoveTowards(transform.position, nextPosition.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPosition.position) < Mathf.Epsilon)
        {
            if (waitedTime >= moveWaitTime)
            {
                nextPosition.position = GenerateRandomPosition();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
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