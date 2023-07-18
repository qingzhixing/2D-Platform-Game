using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip pickCoin;

    public AudioClip useTrashBin;

    public AudioClip playerJump;

    public AudioClip playerInjured;

    public AudioClip playerAttack;

    public AudioClip playerClimb;

    public float climbAudioPlayInterval = 0.3f;

    private AudioSource ownAudioSource;

    private float lastPlayPlayerClimbTime = -100;

    public static AudioController Instance => GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();

    public static void PlayAudio(AudioClip clip)
    {
        Instance.ownAudioSource.PlayOneShot(clip);
    }

    public static void PlayPickCoin()
    {
        PlayAudio(Instance.pickCoin);
    }

    public static void PlayUseTrashBin()
    {
        PlayAudio(Instance.useTrashBin);
    }

    public static void PlayPlayerJump()
    {
        PlayAudio(Instance.playerJump);
    }

    public static void PlayPlayerInjured()
    {
        PlayAudio(Instance.playerInjured);
    }

    public static void PlayPlayerAttack()
    {
        PlayAudio(Instance.playerAttack);
    }

    public static void PlayPlayerClimb()
    {
        if (Time.time - Instance.lastPlayPlayerClimbTime > Instance.climbAudioPlayInterval)
        {
            Instance.lastPlayPlayerClimbTime = Time.time;
            PlayAudio(Instance.playerClimb);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ownAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}