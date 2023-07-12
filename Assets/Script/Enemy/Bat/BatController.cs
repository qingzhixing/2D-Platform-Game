using Cinemachine;
using UnityEngine;

public class BatController : MonoBehaviour
{
    // ÊôÐÔ
    public float moveSpeed = 2.0f;

    public float moveWaitTime = 5.0f;

    public short maxItemAmount = 3;

    public GameObject fallingItem;

    public Transform leftDownPositionLimit;

    public Transform rightUpPositionLimit;

    public Transform nextPosition;

    private float waitedTime = 0;

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
            Destroy(gameObject);
        });

        ownEntityController.RegisterOnInjured(() => { CameraShack(); });
        nextPosition.position = GenerateRandomPosition();
    }

    private void GenerateFallingItem()
    {
        if (fallingItem == null) return;
        short amount = (short)Random.Range(0, maxItemAmount + 1);
        for (int generated = 1; generated <= amount; generated++)
        {
            Instantiate(fallingItem, transform.position, Quaternion.identity);
        }
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
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            EntityController otherController = other.gameObject.GetComponent<EntityController>();
            if (otherController != null)
            {
                otherController.TakeDamege(ownEntityController.damage);
            }
        }
    }
}