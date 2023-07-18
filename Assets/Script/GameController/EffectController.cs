using UnityEngine;

public class EffectController : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject damageTextEffect;
    public ScreenFlashController bindScreenFlash;

    public static EffectController Instance => GameObject.FindGameObjectWithTag("GameController").GetComponent<EffectController>();

    public void FlashScreen()
    {
        if (bindScreenFlash == null) return;
        bindScreenFlash.FlashScreen();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}