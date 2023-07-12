using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public PlayerController bindPlayer;
    public TMP_Text bindDisplayText;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        bindDisplayText.text = bindPlayer.coinAmount.ToString();
    }
}