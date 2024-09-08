using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    public float speed = 5f;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z) ;
    }
}
