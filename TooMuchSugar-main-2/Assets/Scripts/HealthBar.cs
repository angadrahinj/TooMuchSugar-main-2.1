using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Image healthBarSprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        healthBarSprite = GetComponentInChildren<Image>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthBarSprite.fillAmount = maxHealth;
    }

    public void UpdateHealthBar(float maxHealth,float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth/maxHealth;
    }
}
