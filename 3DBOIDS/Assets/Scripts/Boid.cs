using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boid : MonoBehaviour
{
    private Flock group;
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
        group = GetComponent<Flock>();
        rb = GetComponent<Rigidbody>();
        vel = Random.insideUnitSphere * Spawner.SETTINGS.velocity;

        LookAhead();
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

        float fDt = Time.fixedDeltaTime;

        Vector3 sumVel = Vector3.zero;
        Vector3 delta = Attractor.position - pos;

        if (delta.magnitude > bSet.attractPushDist) {
            sumVel += delta.normalized * bSet.attractPull;
        }
        else {
            sumVel -= delta.normalized * bSet.attractPush;
        }


        Vector3 vecAvoid;
        Vector3 tooNearPos = group.avgNearPos;
        if (tooNearPos != Vector3.zero)
        {
            vecAvoid = pos - tooNearPos;
            vecAvoid.Normalize();
            sumVel += vecAvoid * bSet.nearAvoid;
        }

        Vector3 vecAlign = group.avgVel;
        if (vecAlign != Vector3.zero)
        {
            vecAlign.Normalize();

            sumVel += vecAlign * bSet.velMatching;
        }

        Vector3 vecCohes = group.avgPos;
        if (vecCohes != Vector3.zero)
        {
            vecCohes -= transform.position;
            vecCohes.Normalize();
            sumVel += vecCohes * bSet.flockCentering;
        }


        sumVel.Normalize();
        vel = Vector3.Lerp(vel.normalized, sumVel, bSet.velEasing);
        
        vel *= bSet.velocity;

        LookAhead();

    }
}
