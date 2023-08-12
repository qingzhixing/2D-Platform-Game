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

    public AudioClip batDeath;

    public AudioClip batHurt1;

    public AudioClip batHurt2;

    public AudioClip batIdle1;

    public AudioClip batIdle2;

    public AudioClip batIdle3;

    public AudioClip batIdle4;

    public AudioClip batFly;

    public float batFlyVolumnScale = 0.2f;

    public AudioSource globalAudioSource;

    public static AudioController Instance => GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();

    public static void PlayAudio(AudioClip clip)
    {
        Instance.globalAudioSource.PlayOneShot(clip);
    }

    // Start is called before the first frame update
    private void Start()
    {
        globalAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}