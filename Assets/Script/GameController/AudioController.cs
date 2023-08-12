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

    public AudioClip chestOpen;

    public AudioClip chestClose;

    public AudioClip sickleThrow;

    public AudioClip sickleSpin;

    public float spinAudioPlayInterval = 0.3f;

    public AudioClip denied;

    private AudioSource ownAudioSource;

    private float lastPlayPlayerClimbTime = -100;
    private float lastPlaySickleSpinTime = -100;

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

    public static void PlayChestOpen()
    {
        PlayAudio(Instance.chestOpen);
    }

    public static void PlayChestClose()
    {
        PlayAudio(Instance.chestClose);
    }

    public static void PlaySickleThrow()
    {
        PlayAudio(Instance.sickleThrow);
    }

    public static void PlayDenied()
    {
        PlayAudio(Instance.denied);
    }

    public static void PlaySickleSpin()
    {
        if (Time.time - Instance.lastPlaySickleSpinTime > Instance.spinAudioPlayInterval)
        {
            Instance.lastPlaySickleSpinTime = Time.time;
            PlayAudio(Instance.sickleSpin);
        }
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