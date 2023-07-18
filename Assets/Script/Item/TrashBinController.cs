using UnityEngine;

public class TrashBinController : MonoBehaviour
{
    public bool playerEntered = false;

    public short maxCoinAmount = 10;
    public short currentCoinAmount = 0;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerEntered && PlayerController.PlayerInteracted)
        {
            PlayerController player = PlayerController.Instance;
            if (player.coinAmount > 0 && currentCoinAmount < maxCoinAmount)
            {
                currentCoinAmount++;
                player.coinAmount--;
                AudioController.PlayUseTrashBin();
            }
        }
    }
}