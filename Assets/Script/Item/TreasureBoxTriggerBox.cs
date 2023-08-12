using UnityEngine;

public class TreasureBoxTriggerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            GetComponentInParent<TreasureBoxController>().playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            GetComponentInParent<TreasureBoxController>().playerEntered = false;
        }
    }
}