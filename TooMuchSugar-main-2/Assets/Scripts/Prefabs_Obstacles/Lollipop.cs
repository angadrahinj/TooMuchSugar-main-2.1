using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Lollipop : MonoBehaviour
{
    public float rotateSpeed;
    public int rotationDirection = 1;
    public float limit = 75f;
    private float angle;


    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        angle = limit * Mathf.Sin(Time.time * rotateSpeed * rotationDirection);
        // Debug.Log(angle);
        transform.localRotation = Quaternion.Euler(0,0,angle);

        // transform.Rotate(transform.forward * rotateSpeed * rotationDirection);
    }
}
