using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Flying/Idle State")]
public class D_FlyingIdleState : ScriptableObject
{
    public float moveSpeed = 2f;
    public float rangeOfMotion = 0.5f;
}
