using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    Transform[] wayPoints;

    private Transform currentWayPoint;
    private int num = 0;
    [SerializeField] float speed = 10f;

    void Start()
    {
        currentWayPoint = wayPoints[0];
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, wayPoints[num].position) <= 0.02f)
        {
            num ++;

            if(num >= wayPoints.Length)
            {
                num = 0;
            }

        }
        
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[num].position, speed * Time.deltaTime);
    }
}
