using UnityEngine;

public class TrashBinTriggerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            GetComponentInParent<TrashBinController>().playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            GetComponentInParent<TrashBinController>().playerEntered = false;
        }
    }
}