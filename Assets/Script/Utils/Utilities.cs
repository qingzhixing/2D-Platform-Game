using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void SetRandomSpeed2D(GameObject gameObject, Vector2 from, Vector2 to)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D == null) return;
        Vector2 randomVelocity = new Vector2(Random.Range(from.x, to.x), Random.Range(from.y, to.y));
        rigidbody2D.velocity = randomVelocity;
    }
}