using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    Boid[] boids;
    public FlockSettings settings;
    public Transform target;
    
    void Start()
    {
        boids = FindObjectsOfType<Boid>();
        foreach (Boid b in boids)
        {
            b.Initialise(settings, target);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public struct FlockData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
