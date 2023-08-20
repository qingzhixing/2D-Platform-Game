using UnityEngine;
using UnityEngine.EventSystems;

public class EnterButtonController : MonoBehaviour, IDragHandler
{
    public float followSpeed = 0.3f;
    private RectTransform rectTransform;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 position = Vector3.Lerp(rectTransform.position, Input.mousePosition, followSpeed * Time.deltaTime);
        rectTransform.position = position;
    }
}