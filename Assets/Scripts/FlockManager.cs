using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlockManager : MonoBehaviour
{
    [SerializeField] Boid prefab;
    public List<Boid> boids;
    public Color chosenColor;

    public FlockSettings settings;
    public Transform target;
    public Octree octree;
    public Vector3 boundingArea;

    public bool showBounds;
    public bool showSpawnArea;
    void Start()
    {
        for (int i = 0; i < settings.spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * settings.spawnRadius;

            Boid b = Instantiate(prefab);

            b.SetColour(chosenColor);
            b.Initialise(settings, target);

            b.transform.position = pos;
            b.transform.forward = Random.insideUnitSphere;
            boids.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.35f, 0.3f);
        if (showSpawnArea)
        {
            Gizmos.DrawSphere(transform.position, settings.spawnRadius);
        }

        Gizmos.color = new Color(0.0f, 0.8f, 0.35f, 0.3f);
        if (showBounds)
        {
            Gizmos.DrawCube(transform.position, boundingArea);
        }
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
