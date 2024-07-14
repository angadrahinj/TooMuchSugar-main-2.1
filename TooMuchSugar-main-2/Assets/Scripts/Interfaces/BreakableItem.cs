using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakableItem : MonoBehaviour, IDamageable
{
    public float health;

    public virtual void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
