using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject damageTextEffect;
    public ScreenFlashController bindScreenFlash;

    public static GameController Instance => GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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