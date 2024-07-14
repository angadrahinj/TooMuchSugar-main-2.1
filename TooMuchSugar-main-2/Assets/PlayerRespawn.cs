using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private bool alreadyEntered = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyEntered)
        return;

        if(other.gameObject.tag == "Player")
        {
            PlayerRespawnManager.instance.SetRespawnPoint(this.transform.position);
        }
    }
}
