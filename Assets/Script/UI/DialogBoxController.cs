using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxController : MonoBehaviour
{
    public TMP_Text bindText;
    private Image ownImage;

    public void Display()
    {
        ownImage.enabled = true;
        bindText.enabled = true;
    }

    public void Display(string text)
    {
        ChangeText(text);
        Display();
    }

    public void Trigger()
    {
        if (ownImage.enabled)
        {
            Hide();
        }
        else
        {
            Display();
        }
    }

    public void Trigger(string text)
    {
        if (ownImage.enabled)
        {
            Hide();
        }
        else
        {
            Display(text);
        }
    }

    public void Hide()
    {
        ownImage.enabled = false;
        bindText.enabled = false;
    }

    public void ChangeText(string text)
    {
        bindText.text = text;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ownImage = GetComponent<Image>();
        Hide();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}