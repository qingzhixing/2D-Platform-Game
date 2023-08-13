using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector2 initSpeed;

    // ���õô�һ���Է�һ���ɾͱ�ը��
    public float explodeCountdown = 100;

    public float deleteTime = 1.2f;

    // ��ը���������ӳ�
    public float explodeAudioPlayDelay = 0.5f;

    private float timer;

    private Rigidbody2D ownRigidbody;

    private Animator ownAnimator;

    private bool isExploded = false;

    // Start is called before the first frame update
    private void Start()
    {
        ownRigidbody = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();

        ownRigidbody.velocity = transform.right * initSpeed.x + transform.up * initSpeed.y;

        StartCoroutine(PlayBombFuse());
    }

    private IEnumerator PlayBombFuse()
    {
        // ��ֹ��AudioController��û�������ʱ���ò������ֵĹ���
        yield return new WaitForSeconds(0.3f);
        AudioControllerHelpers.PlayBombFuse();
    }

    // Update is called once per frame
    private void Update()
    {
        CountdownHandler();
        ExplodeHandler();
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

    private void ExplodeHandler()
    {
        if (timer < explodeCountdown) return;
        if (!isExploded)
        {
            isExploded = true;
            StartCoroutine(PlayBombExplode());
            StartCoroutine(Explode());
        }
    }

    private IEnumerator PlayBombExplode()
    {
        yield return new WaitForSeconds(explodeAudioPlayDelay);
        AudioControllerHelpers.PlayBombExplode();
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(gameObject);
    }

    private void CountdownHandler()
    {
        timer += Time.deltaTime;
    }
}