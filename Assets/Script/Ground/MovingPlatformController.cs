using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    public float waitTime = 3;

    public Transform[] movePositions;

    public Utilities.Direction moveDirection = Utilities.Direction.Forward;

    public MoveMode moveMode = MoveMode.LinearLoop;

    private int nextPositionId = 0;

    private float waitedTime = 0;

    private Transform originalPlayerParentTransform = null;

    public enum MoveMode
    {
        CircleLoop, LinearLoop
    }

    // Start is called before the first frame update
    private void Start()
    {
        originalPlayerParentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHandler();
    }

    private void MoveHandler()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePositions[nextPositionId].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePositions[nextPositionId].position) < Mathf.Epsilon)
        {
            if (waitedTime < waitTime)
            {
                waitedTime += Time.deltaTime;
            }
            else
            {
                waitedTime = 0;
                if (moveMode == MoveMode.LinearLoop)
                {
                    if (nextPositionId == movePositions.Length - 1)
                    {
                        moveDirection = Utilities.Direction.Backward;
                    }
                    else if (nextPositionId == 0)
                    {
                        moveDirection = Utilities.Direction.Forward;
                    }
                }
                nextPositionId += (moveDirection == Utilities.Direction.Forward) ? 1 : -1;
                if (moveMode == MoveMode.CircleLoop)
                {
                    if (nextPositionId >= movePositions.Length && moveDirection == Utilities.Direction.Forward)
                    {
                        nextPositionId = 0;
                    }
                    else if (nextPositionId <= -1 && moveDirection == Utilities.Direction.Backward)
                    {
                        nextPositionId = movePositions.Length - 1;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.SetParent(originalPlayerParentTransform);
        }
    }
}