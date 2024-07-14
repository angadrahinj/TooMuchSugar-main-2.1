using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Health Related Variables")]
    public int currentHealth;
    public int maxHealth = 4;

    public float damageCoolDownTime = 2f;

    public bool canTakeDamage = true;



    void Awake()
    {
        currentHealth = maxHealth;
    }

    IEnumerator DamageCoolDown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCoolDownTime);
        canTakeDamage = true;
    }

    public void Damage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
        }
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // get a parameter for spawn point in die.
    }

}
