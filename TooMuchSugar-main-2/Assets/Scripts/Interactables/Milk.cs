using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public float regenHealthAmount = 30f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().AddHealth(regenHealthAmount);
            LevelManager.instance.CollectMilkBottle();
            Destroy(gameObject);
        }
    }
}
