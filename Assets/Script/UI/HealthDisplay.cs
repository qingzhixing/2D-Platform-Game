using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public TMP_Text healthText;
    public EntityController bindEntity;
    public Image bindHPBarImage;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        bindHPBarImage.fillAmount = bindEntity.currentHealth / bindEntity.maxHelth;
        healthText.text = string.Format("{0} / {1}", bindEntity.currentHealth, bindEntity.maxHelth);
    }
}