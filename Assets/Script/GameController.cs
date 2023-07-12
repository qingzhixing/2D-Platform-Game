using UnityEngine;

public class GameController : MonoBehaviour
{
    public ScreenFlashController bindScreenFlash;

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