using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Boid : MonoBehaviour
{
    FlockSettings settings;
    
    public Material wingMat;
    public Material fuseMat;

    Transform cachedTransform;
    Transform cachedTarget;

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 forward;
        
    public Vector3 acceleration;
    int numWithinFlock;

    Vector3 centerOfMass;
    Vector3 avgSeperateDir;
    Vector3 avgFlockHeading;

    //bool isPerched = false;
    //bool isPerching = false;
    private void Awake()
    {
        
        cachedTransform = transform;
    }
    Vector3 SteerToward(Vector3 vector)
    {
        Vector3 steerVector = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(steerVector,settings.steeringForce);
    }
    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }
        return forward;
    }
    public void SetColour(Color color)
    {
        if (wingMat != null && fuseMat!= null)
        {
            wingMat.color = color;
            fuseMat.color = color;
        }
    }

    public void Initialise(FlockSettings setting, Transform target)
    {
        this.settings = setting;
        this.cachedTarget = target;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed * settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask))
        {
            return true;
        }
        else { }
        return false;
    }

   
    void Update()
    {
        acceleration= Vector3.zero;  
        
        if(cachedTarget != null)
        {
            Vector3 offset = (cachedTarget.position - position);
            acceleration = SteerToward(offset) * settings.targetingWeight;
        }

        if (numWithinFlock != 0)
        {
            centerOfMass /= numWithinFlock;
            Vector3 offsetToCenter = (centerOfMass - position);

            var cohesion = SteerToward(centerOfMass) * settings.cohesionWeight;
            var alignment = SteerToward(avgFlockHeading) * settings.alignWeight;
            var seperation = SteerToward(avgSeperateDir) * settings.seperateWeight;

            acceleration += cohesion;
            acceleration += alignment;
            acceleration += seperation;
        }

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerToward(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position=cachedTransform.position;
        forward = dir;

    }

    
}
