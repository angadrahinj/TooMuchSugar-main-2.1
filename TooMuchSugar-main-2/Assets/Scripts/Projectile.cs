using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDirection;
    private int shootSpeed = 3;

    public void Initialize(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
    }

    public void FixedUpdate()
    {
        this.transform.position += shootDirection * shootSpeed * Time.deltaTime;
    }
}
