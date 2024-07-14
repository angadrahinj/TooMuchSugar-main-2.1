using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IDamageable
{
    [SerializeField] Transform lever;
    [SerializeField] Door door;
    private bool isDoorOpen = false;
    public bool openOnce = true;

    void Awake()
    {
        // door =  GetComponent<Door>();
    }

    public void Damage(float damage)
    {
        if(!isDoorOpen)
        {
            door.OpenDoor();
            SmoothRotation(-45f);
        }
        else if (!openOnce)
        {
            door.CloseDoor();
            SmoothRotation(45f);
        }
        isDoorOpen = !isDoorOpen;
    }

    public void Die()
    {
        
    }

    void SmoothRotation(float angle)
    {
        lever.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
