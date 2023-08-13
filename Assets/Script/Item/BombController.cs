using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector2 initSpeed;

    // 设置得大一点以防一生成就爆炸了
    public float explodeCountdown = 100;

    public float deleteTime = 1.2f;

    // 爆炸声音播放延迟
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
        // 防止在AudioController还没构造完成时调用播放音乐的功能
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
        // 爆炸动画
        if (timer >= explodeCountdown)
        {
            // 恢复正常速度
            ownAnimator.speed = 1;
            ownAnimator.SetTrigger("Explode");
        }
        else
        {
            // 否则设置闪烁动画播放速度加快
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