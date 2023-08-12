using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool playerEntered = false;
    public bool isOpen = false;
    public bool enableOpen = true;

    public TrophyController trophyController = null;

    private Animator ownAnimator;

    public void Start()
    {
        ownAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        InteractionHandler();
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        if (ownAnimator.GetBool("Opened") == true && isOpen == false)
        {
            ownAnimator.SetBool("Opened", false);
            AudioController.PlayChestClose();
        }
        if (ownAnimator.GetBool("Opened") == false && isOpen == true)
        {
            ownAnimator.SetBool("Opened", true);
            AudioController.PlayChestOpen();
        }
    }

    private void InteractionHandler()
    {
        if (!playerEntered || !PlayerController.PlayerInteracted) return;
        if (!isOpen && enableOpen)
        {
            if (ownAnimator != null)
            {
                isOpen = true;
                GenerateItem();
                AudioController.PlayChestOpen();
            }
        }
        else
        {
            AudioController.PlayDenied();
        }
    }

    private void GenerateItem()
    {
        trophyController.GenerateTrophies(gameObject);
    }
}