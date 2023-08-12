using UnityEngine;

public class TrashBinController : MonoBehaviour
{
    public bool playerEntered = false;

    public short maxCoinAmount = 10;
    public short currentCoinAmount = 0;

    public TrophyController trophyController = null;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        InteractionHandler();
    }

    private void InteractionHandler()
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
            // 投币满再互动触发奖励
            else if (currentCoinAmount >= maxCoinAmount)
            {
                currentCoinAmount = 0;
                trophyController.GenerateTrophies(gameObject);
            }
        }
    }
}