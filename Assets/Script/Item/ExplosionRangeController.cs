using UnityEngine;

public class ExplosionRangeController : MonoBehaviour
{
    public float damage = 2f;
    public float destroyTime = 0.1f;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        EntityController entityController = other.gameObject.GetComponent<EntityController>();
        if (entityController != null)
        {
            entityController.TakeDamege(damage);
        }
    }
}