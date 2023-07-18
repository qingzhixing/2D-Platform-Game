using UnityEngine;

public class MonoStub : MonoBehaviour
{
    public Component currentComponent;
    public float maxRunTime = 0.1f;

    public void StartDestroy()
    {
        Destroy(currentComponent, maxRunTime);
    }
}