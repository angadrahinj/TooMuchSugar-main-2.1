using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopDamage : MonoBehaviour
{
    public float damage = 10f;
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Touching player");
            other.gameObject.GetComponent<PlayerHealthManager>().Damage(damage);
        }
    }
}
