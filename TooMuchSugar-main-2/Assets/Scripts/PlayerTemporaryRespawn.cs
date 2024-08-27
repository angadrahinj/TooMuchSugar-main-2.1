using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTemporaryRespawn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerRespawnManager.instance.SetTemporaryRespawnPoint(this.transform.position);
        }
    }
}
