using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    private AudioSource ownAudioSource;

    // ��Unity����
    public void OnEnterButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ��Unity����
    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    private void Start()
    {
        ownAudioSource = GetComponent<AudioSource>();
        ownAudioSource.playOnAwake = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}