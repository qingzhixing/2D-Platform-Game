using System.Collections;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    private static GameObject instance;

    public enum Direction
    {
        Forward, Backward
    }

    public static void SetRandomSpeed2D(GameObject gameObject, Vector2 from, Vector2 to)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D == null) return;
        Vector2 randomVelocity = new Vector2(Random.Range(from.x, to.x), Random.Range(from.y, to.y));
        rigidbody2D.velocity = randomVelocity;
    }

    public static GameObject GetProxyGameObject()
    {
        if (instance == null)
        {
            instance = new GameObject("Utilities Runtime Object");
        }
        return instance;
    }

    // 传入Coroutine,和该Coroutine最大运行时间
    public static void StartCoroutine(IEnumerator enumerator, float maxRunTime = 0.1f)
    {
        MonoStub component = ConstructMonoStub(maxRunTime);
        component.StartCoroutine(enumerator);
        component.StartDestroy();
    }

    private static MonoStub ConstructMonoStub(float maxRunTime = 0.1f)
    {
        MonoStub component = GetProxyGameObject().AddComponent<MonoStub>();
        component.currentComponent = component;
        component.maxRunTime = maxRunTime;
        return component;
    }
}