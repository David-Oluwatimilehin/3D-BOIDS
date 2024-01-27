using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    public Vector3 vel
    {
        get { return rb.velocity; }
        set { rb.velocity = value; }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vel = Random.insideUnitSphere * Spawner.SETTINGS.velocity;

       
        ChangeColour();
    }

    private void LookAhead()
    {
        transform.LookAt(pos+rb.velocity);
    }
    

    private void ChangeColour()
    {
        Color randColour = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
        
        // Set Wing and Fuselage to same colour
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends) {
            r.material.color = randColour;

        }

        TrailRenderer trend = GetComponent<TrailRenderer>();
        trend.startColor = randColour;
        randColour.a = 0;
        trend.endColor = randColour;
        trend.endWidth = 0;


    }
    private void FixedUpdate()
    {
        BoidSettings bSet=Spawner.SETTINGS;

        Vector3 sumVel = Vector3.zero;
        Vector3 delta = Attractor.position - pos;

        if (delta.magnitude>bSet.attractPushDist) {
            sumVel += delta.normalized * bSet.attractPull;
        }
        else {
            sumVel -= delta.normalized * bSet.attractPush;
        }

        sumVel.Normalize();
        vel = Vector3.Lerp(vel.normalized, sumVel, bSet.velEasing);
        
        vel *= bSet.velocity;

        LookAhead();

    }
}
