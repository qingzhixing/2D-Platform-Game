using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashBinCoinDisplay : MonoBehaviour
{
    public RectTransform CanvasRect;
    public TrashBinController bindTrasnBin;
    public float xOffset = 0;
    public float yOffset = 25;

    public TMP_Text bindText;
    public Image bindFrame;
    public Image bindBar;

    private RectTransform ownRectTransform;

    // Start is called before the first frame update
    private void Start()
    {
        ownRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateWorldPosition();
        UpdateDisplay();
        SwitchHandler();
    }

    private void Hide()
    {
        bindText.enabled = false;
        bindBar.enabled = false;
        bindFrame.enabled = false;
    }

    private void Display()
    {
        bindText.enabled = true;
        bindBar.enabled = true;
        bindFrame.enabled = true;
    }

    private void SwitchHandler()
    {
        if (bindTrasnBin.playerEntered)
        {
            Display();
        }
        else
        {
            Hide();
        }
    }

    private void UpdateDisplay()
    {
        bindText.text = string.Format("{0} / {1}", bindTrasnBin.currentCoinAmount, bindTrasnBin.maxCoinAmount);
        bindBar.fillAmount = (float)bindTrasnBin.currentCoinAmount / bindTrasnBin.maxCoinAmount;
    }

    private void UpdateWorldPosition()
    {
        Vector2 trashBinViewPosition = Camera.main.WorldToViewportPoint(bindTrasnBin.transform.position);
        float xWorldPos = (trashBinViewPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f) + xOffset;
        float yWorldPos = (trashBinViewPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f) + yOffset;
        Vector2 ownNewWorldPosition = new Vector2(xWorldPos, yWorldPos);
        ownRectTransform.anchoredPosition = ownNewWorldPosition;
    }
}