using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashController : MonoBehaviour
{
    public float flashTime = 0.3f;
    public float maxAlpha = 0.5f;
    private Image ownImage;

    public void FlashScreen()
    {
        IEnumerator StartFlash()
        {
            Color originalColor = ownImage.color;
            // ÏÔÉ«
            for (float spendTime = 0; spendTime < 0.5 * flashTime; spendTime += Time.deltaTime)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                float lerpRate = spendTime / (0.5f * flashTime);
                ownImage.color = Color.Lerp(originalColor, Color.white, lerpRate * maxAlpha);
            }
            // ÏûÉ«
            for (float spendTime = 0; spendTime < 0.5 * flashTime; spendTime += Time.deltaTime)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                float lerpRate = 1 - (spendTime / (0.5f * flashTime));
                ownImage.color = Color.Lerp(originalColor, Color.white, lerpRate * maxAlpha);
            }
            ownImage.color = originalColor;
        }

        StartCoroutine(StartFlash());
    }

    // Start is called before the first frame update
    private void Start()
    {
        ownImage = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}