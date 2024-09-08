using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterKill : MonoBehaviour
{
    public float liquidDamageAmount = 10f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthManager healthManager = PlayerRespawnManager.instance.player.GetComponent<Player>().GetComponent<PlayerHealthManager>();
            healthManager.Damage(liquidDamageAmount);

            float playerCurrentHealth = healthManager.ReturnHealth();
            if (playerCurrentHealth <= 0)
            {
                healthManager.Die();
            }
            else
            {
                PlayerRespawnManager.instance.StartTemporaryRespawnSequence();
            }
        }
    }
}
