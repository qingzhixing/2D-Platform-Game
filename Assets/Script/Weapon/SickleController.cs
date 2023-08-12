using UnityEngine;

public class SickleController : MonoBehaviour
{
    public float flySpeed = 10f;
    public float flyTime = 1;
    public float damage = 1.5f;
    public float rotateSpeed = 15;
    public float yTuringSpeed = 0.01f;
    public Utilities.Direction direction = Utilities.Direction.Forward;
    private Rigidbody2D ownRigidbody2D;
    private Transform playerTransfrom;

    // Start is called before the first frame update
    private void Start()
    {
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, 0);

        ownRigidbody2D.velocity = transform.right * flySpeed * (direction == Utilities.Direction.Forward ? 1 : -1);
        playerTransfrom = PlayerController.Instance.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHandler();
        AudioHandler();
    }

    private void AudioHandler()
    {
        AudioController.PlaySickleSpin();
    }

    private void MoveHandler()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, rotateSpeed);

        ownRigidbody2D.velocity -= Vector2.right * flySpeed * (direction == Utilities.Direction.Forward ? 1 : -1) * Time.deltaTime / flyTime;

        // 回旋镖回来时才与player同步y轴
        if (ownRigidbody2D.velocity.x * (direction == Utilities.Direction.Forward ? 1 : -1) < 0)
        {
            float newY = Mathf.Lerp(transform.position.y, playerTransfrom.position.y, yTuringSpeed);
            transform.position = new Vector3(transform.position.x, newY, 0);
        }

        // Forward判断飞回0.5f,Backward判断飞到前面0.5f,没飞过之前二者乘积均为负数
        if ((transform.position.x - playerTransfrom.position.x) * (direction == Utilities.Direction.Forward ? -1 : 1) > 20 * flySpeed * Time.deltaTime)
        {
            // 飞过头
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EntityController entityController = other.GetComponent<EntityController>();
        if (entityController != null)
        {
            entityController.TakeDamege(damage);
        }
    }
}