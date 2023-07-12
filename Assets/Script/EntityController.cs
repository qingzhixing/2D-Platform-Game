using System.Collections;
using UnityEngine;

// 用来管理实体的基本属性
public class EntityController : MonoBehaviour
{
    public float maxHelth = 10;

    public float currentHealth = 10;

    public float damage = 1;

    public float attackInterval = 0.5f;

    public float invincibleTime = 0.5f;

    public bool enableInjuredFlash = true;

    public float flashTime = 0.3f;

    public Color flashColor = Color.red;

    public bool enableBloodEffect = true;

    public GameObject bloodEffect = null;

    private float lastInjuredTime = -10;

    // 字段
    private DeathHook onDeath;

    private InjuredHook onInjured;

    public delegate void DeathHook();

    public delegate void InjuredHook();

    public bool IsDead => currentHealth <= 0;

    public void RegisterOnDeath(DeathHook onDeath)
    {
        this.onDeath = onDeath;
    }

    public void RegisterOnInjured(InjuredHook onInjured)
    {
        this.onInjured = () =>
        {
            onInjured();
            OnInjuredEffects();
        };
    }

    public void TakeDamege(float damage)
    {
        if (IsDead) return;
        if (Time.time - lastInjuredTime < invincibleTime) return;
        lastInjuredTime = Time.time;

        if (onInjured != null)
        {
            onInjured();
        }
        else
        {
            onInjured = OnInjuredEffects;
        }

        currentHealth -= damage;

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

    private void BloodEffectHandler()
    {
        if (!enableBloodEffect) return;
        if (bloodEffect == null) return;
        Instantiate(bloodEffect, transform);
    }

    // 受伤特效
    private void OnInjuredEffects()
    {
        StartCoroutine(FlashColor());
        BloodEffectHandler();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}