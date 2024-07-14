using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRespawn : MonoBehaviour
{
    void Start()
    {
        PlayerRespawnManager.instance.SetRespawnPoint(this.transform.position);
        // Debug.Log(this.transform.position);
    }
}
