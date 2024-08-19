using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FlockSettings : ScriptableObject
{
    [Header("Steering")]
    public float minSpeed = 2f;
    public float maxSpeed = 6f;
    public float steeringForce = 3f;
    
    
    [Header("Behaviours")]
    public float alignWeight = 1f;
    public float cohesionWeight = 1f;
    public float seperateWeight = 1f;
    public float targetingWeight = 1f;

    //public float targetWeight = 1;

    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = 0.5f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;


}
