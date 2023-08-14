using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public bool playerEntered = false;
    public Utilities.Direction direction;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerEntered && PlayerController.PlayerInteracted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (direction == Utilities.Direction.Forward ? 1 : -1));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == PlayerController.playerBodyComponentName)
        {
            playerEntered = false;
        }
    }
}