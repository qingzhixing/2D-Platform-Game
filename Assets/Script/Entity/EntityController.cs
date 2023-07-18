using System.Collections;
using TMPro;
using UnityEngine;

// 用来管理实体的基本属性
public class EntityController : MonoBehaviour
{
    public float maxHelth = 10;

    public float currentHealth = 10;

    public float damage = 1;

    public Utilities.Direction facingDirection = Utilities.Direction.Forward;

    public float attackInterval = 0.5f;

    public float invincibleTime = 0.5f;

    public bool enableInjuredFlash = true;

    public float flashTime = 0.3f;

    public Color flashColor = Color.red;

    public bool enableBloodEffect = true;

    public bool enableDamageTextEffect = true;

    public Color damageTextColor = Color.red;

    private float lastInjuredTime = -10;

    // 字段
    private DeathHook onDeath;

    private InjuredHook onInjured;

    public delegate void DeathHook();

    public delegate void InjuredHook(float damage);

    public bool IsDead => currentHealth <= 0;

    public void RegisterOnDeath(DeathHook onDeath)
    {
        this.onDeath = onDeath;
    }

    public void RegisterOnInjured(InjuredHook onInjured)
    {
        this.onInjured = (damage) =>
        {
            onInjured(damage);
            OnInjuredEffects(damage);
        };
    }

    public void TakeDamege(float damage)
    {
        if (IsDead) return;
        if (Time.time - lastInjuredTime < invincibleTime) return;
        lastInjuredTime = Time.time;

        if (onInjured != null)
        {
            onInjured(damage);
        }
        else
        {
            onInjured = OnInjuredEffects;
        }

        currentHealth -= damage;
    }

    private IEnumerator FlashColor()
    {
        if (!enableInjuredFlash) yield break;

        SpriteRenderer ownSpriteRenderer = GetComponent<SpriteRenderer>();
        Color originalRenderColor = ownSpriteRenderer.color;
        for (float spendTime = 0; spendTime < flashTime; spendTime += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            float alpha = Mathf.Abs(Mathf.Sin(spendTime * 10));
            ownSpriteRenderer.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
        }
        ownSpriteRenderer.color = originalRenderColor;
    }

    private void DamageTextEffectHandler(float damage)
    {
        if (!enableDamageTextEffect) return;
        if (EffectController.Instance.damageTextEffect == null) return;
        GameObject textEffect = Instantiate(EffectController.Instance.damageTextEffect, transform.position, Quaternion.identity);
        TMP_Text textComponent = textEffect.GetComponent<TMP_Text>();
        textComponent.text = damage.ToString();
        textComponent.color = damageTextColor;
        Utilities.SetRandomSpeed2D(textEffect, new Vector2(-1, -1), new Vector2(1, 1));
    }

    private void BloodEffectHandler()
    {
        if (!enableBloodEffect) return;
        if (EffectController.Instance.bloodEffect == null) return;
        Instantiate(EffectController.Instance.bloodEffect, transform.position, Quaternion.identity);
    }

    // 受伤特效
    private void OnInjuredEffects(float damage)
    {
        StartCoroutine(FlashColor());
        BloodEffectHandler();
        DamageTextEffectHandler(damage);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        DeadHandler();
    }

    private void DeadHandler()
    {
        if (IsDead)
        {
            if (onDeath != null)
            {
                onDeath();
            }
            else
            {
                onDeath = () => { };
            }
        }
    }
}