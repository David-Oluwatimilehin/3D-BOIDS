using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("Dynamic")]

    public List<Boid> flock;
    private SphereCollider col;

    private void Start()
    {
        flock= new List<Boid>();
        col= GetComponent<SphereCollider>();
        col.radius = Spawner.SETTINGS.neighbourDist / 2;
    }

    private void FixedUpdate()
    {
        float nearRadius= Spawner.SETTINGS.neighbourDist * 0.5f;
        if (!Mathf.Approximately(col.radius,nearRadius))
        {
            col.radius=nearRadius;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Boid boid = GetComponent<Boid>();
        if (boid!=null)
        {
            if(!flock.Contains(boid))
            {
                flock.Add(boid);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Boid boid = GetComponent<Boid>();
        if (boid != null)
        {
            flock.Remove(boid);
        }
    }
    public Vector3 avgPos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (flock.Count == 0) return avg;

            for (int i = 0; i < flock.Count; i++)
            {
                avg += flock[i].pos;
            }
            avg /= flock.Count;
            return avg;

        }
    }

    public Vector3 avgVel
    {
        get
        {
            Vector3 avg=Vector3.zero;
            if (flock.Count == 0) return avg;

            for (int i = 0; i < flock.Count; i++)
            {
                avg += flock[i].vel;
            }
            avg/=flock.Count;
            return avg;

        }
    }
    public Vector3 avgNearPos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;

            for (int i = 0; i < flock.Count; i++) {

                delta= flock[i].pos - transform.position;
                if (delta.magnitude <= Spawner.SETTINGS.nearDist)
                {
                    avg += flock[i].pos;
                    nearCount++;
                }
            }
            if(nearCount == 0) return Vector3.zero;

            avg /= nearCount;
            return avg;

        }
    }

}
