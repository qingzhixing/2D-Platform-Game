using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector2 initSpeed;

    // ���õô�һ���Է�һ���ɾͱ�ը��
    public float explodeCountdown = 100;

    public float deleteTime = 1.2f;

    public GameObject explosionRange;

    private float timer;

    private Rigidbody2D ownRigidbody;

    private Animator ownAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        ownRigidbody = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();

        StartCoroutine(DelayInitialize());
    }

    private IEnumerator DelayInitialize()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        AudioControllerHelpers.PlayBombFuse();
        ownRigidbody.velocity = transform.right * initSpeed.x + transform.up * initSpeed.y;
    }

    // Update is called once per frame
    private void Update()
    {
        CountdownHandler();
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        // ��ը����
        if (timer >= explodeCountdown)
        {
            // �ָ������ٶ�
            ownAnimator.speed = 1;
            ownAnimator.SetTrigger("Explode");
        }
        else
        {
            // ����������˸���������ٶȼӿ�
            ownAnimator.speed = Mathf.Lerp(1, 10, timer / explodeCountdown);
        }
    }

    // ��֡�¼�����
    private void ExplodeHandler()
    {
        AudioControllerHelpers.PlayBombExplode();
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    // ��֡�¼�����
    private void DeleteBomb()
    {
        Destroy(gameObject);
    }

    private void CountdownHandler()
    {
        timer += Time.deltaTime;
    }
}