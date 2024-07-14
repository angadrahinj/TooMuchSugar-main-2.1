using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public bool isKnockbackable = true;
    public float entityMaxHealth = 3;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float minAggroDistance = 2f;
    public float maxAggroDistance = 4f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
