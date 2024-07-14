using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public void OpenDoor()
    {
        //handle animations
        Debug.Log("Opening Door");
        this.gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door");
    }
}
