using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    public float waitTime = 2f;

    [SerializeField] Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hi");
            // other.gameObject.GetComponent<PlayerTeleport>().Teleport(teleportLocation.position, waitTime, offset);
        }
    }
}
