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
    
    void Start()
    {
        Bounds newBounds = new Bounds(transform.position, new Vector3(boundingArea.x, boundingArea.y, boundingArea.z));
        
        octree = new Octree(newBounds, maxBoidsPerNode: 50, minNodeSize: 1f);

        Vector3 cubeCenter = transform.position;
        

        for (int i = 0; i < settings.spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * settings.spawnRadius;

            Boid b = Instantiate(prefab);

            b.SetColour(chosenColor);
            b.Initialise(settings, target, octree);

            b.transform.position = pos;
            b.transform.forward = Random.insideUnitSphere;
            boids.Add(b);

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        octree.Clear();
       
        foreach (var b in boids)
        {
            octree.Insert(b);
        }

        foreach (var b in boids)
        {
            b.Update();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 0.7f, 0.35f, 0.3f);
        Gizmos.DrawCube(transform.position, boundingArea);
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
