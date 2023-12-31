using UnityEngine;

public class SignController : MonoBehaviour
{
    public DialogBoxController dialogBox;

    [Multiline(4)]
    public string signText = "fk U!";

    public bool needInteract = true;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            if (PlayerController.PlayerInteracted)
            {
                dialogBox.Trigger(signText);
            }
            else if (!needInteract)
            {
                dialogBox.Display(signText);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            dialogBox.Hide();
        }
    }
}