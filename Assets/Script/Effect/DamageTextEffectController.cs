using UnityEngine;

public class DamageTextEffectController : MonoBehaviour
{
    public float timeToDestroy = 1;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}