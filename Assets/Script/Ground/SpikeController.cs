using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float damage = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() ==  PlayerController.playerBodyComponentName)
        {
            EntityController otherController = other.GetComponent<EntityController>();
            if (otherController != null)
            {
                otherController.TakeDamege(damage);
            }
        }
    }
}