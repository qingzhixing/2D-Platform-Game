using UnityEngine;

public class PlayerNormalAttackController : MonoBehaviour
{
    public float damage = 1;
    // ×é¼þ

    private PolygonCollider2D ownCollider2D;

    // Start is called before the first frame update
    private void Start()
    {
        ownCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EntityController otherEntityController = other.GetComponent<EntityController>();
            if (otherEntityController != null)
            {
                otherEntityController.TakeDamege(damage);
            }
        }
    }
}