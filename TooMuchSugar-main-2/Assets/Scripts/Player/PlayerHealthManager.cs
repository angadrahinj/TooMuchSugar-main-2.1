using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] Player player;
    [SerializeField] PlayerData playerData;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color damagedColor = Color.red;
    private Color color;
    private float alpha;
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaMultiplier = 0.85f;

    public float health;
    public bool canDamage = true;
    public float damageCooldown = 2f;
    public float damageAmount = 10f;
    public float damageEffectCounter = 0;

    

    void Awake()
    {
        player = GetComponent<Player>();
        health = playerData.maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        spriteRenderer.color = Color.white;
        canDamage = true;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Damage(float damage)
    {
        if (canDamage)
        {
            health -= damage;

            UpdateHealthUI();
            // Debug.Log("Damaged player: Player's Health = " + health);
            // Debug.Log(health);

            if (health <= 0)
            {
                Die();
                return;
            }
            else{
                StartCoroutine(DamageEffect());
            }


            // Run coroutine for damage cooldown
            StartCoroutine(DamageCooldown());
        }
    }

    public void SetHealth(float health)
    {
        this.health = health;
        UpdateHealthUI();
    }

    public void AddHealth(float health)
    {
        this.health += health;

        this.health = Mathf.Clamp(this.health, 0f, playerData.maxHealth);

        UpdateHealthUI();
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        // Debug.Log("Cannot damage player");
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
        // Debug.Log("Can damage player");
    }

    public IEnumerator DamageEffect()
    {
        float flashDelay = 0.0833f;
        var secondsToWait = new WaitForSeconds(flashDelay);

        alpha = alphaSet;
        while (damageEffectCounter <= 1.0f)
        {
            damageEffectCounter += 0.25f;

            alpha *=  alphaMultiplier;
            // Debug.Log(alpha);

            color = new Color(damagedColor.r, damagedColor.g, damagedColor.b, alpha);

            spriteRenderer.color = color;

            yield return secondsToWait;
            spriteRenderer.color = defaultColor;
            yield return secondsToWait;
        }
        damageEffectCounter = 0;
        spriteRenderer.color = defaultColor;
    }

    public void Die()
    {
        player.transform.position = PlayerRespawnManager.instance.currentRespawnPoint;
        StopAllCoroutines();
        if (health <= 0)
        {
            health = playerData.maxHealth;
        }
        PlayerRespawnManager.instance.StartDeathSequence();
    }

    public float ReturnHealth()
    {
        return health;
    }

    public void UpdateHealthUI()
    {
        HealthBar.instance.UpdateHealthBar(playerData.maxHealth, health);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            Damage(damageAmount);
        }
    }
}
