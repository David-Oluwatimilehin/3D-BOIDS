using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Boid Settings")]
    [SerializeField] Boid prefab;
    [SerializeField] int spawnCount;
    public Color chosenColor;

    [Header("Spawn Area")]
    [SerializeField] float spawnRadius;

    [SerializeField] int lengthX;
    [SerializeField] int lengthY;
    [SerializeField] int lengthZ;

    //[Header("Visualisation Settings")]
    //[SerializeField] bool showCohesion;
    //[SerializeField] bool showAlignment;
    //[SerializeField] bool showSeperation;

    //private Boid[] boidArray;
    private Vector3 cubeSize;

    private Vector3 maxBoundingDist;
    private Vector3 minBoundingDist;
    void CreateFlock()
    {
            

        minBoundingDist = transform.position - cubeSize / 2;
        maxBoundingDist = transform.position + cubeSize / 2;

        float directionFacing = 0;
        
        
        for (int i = 0; i<spawnCount; i++)
        {
            directionFacing = Random.Range(0f, 360f);

            //spawnPos = new Vector3(
            //Random.Range(cubeCenter.x - cubeSize.x / 2, cubeCenter.x + cubeSize.x / 2),
            //Random.Range(cubeCenter.y - cubeSize.y / 2, cubeCenter.y + cubeSize.y / 2),
            //Random.Range(cubeCenter.z - cubeSize.z / 2, cubeCenter.z + cubeSize.z / 2));

            //prefab = Instantiate(prefab, spawnPos, Quaternion.Euler(new Vector3(0f, directionFacing, 0f)));
            //prefab.transform.SetParent(transform, false);
            prefab.name = "Particle" + (int) Random.Range(0, (i+1));

            //boidArray[i] = prefab.GetComponent<Boid>();
            //prefab.GetComponent<Boid>().position = spawnPos;
            
        }
    }
    private void Awake()
    {
        Vector3 cubeCenter = transform.position;
        cubeSize = new Vector3(lengthX, lengthY, lengthZ);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;

            Boid b = Instantiate(prefab);
            
            b.SetColour(chosenColor);

            //b.GetC<MeshRenderer>().material.color = chosenColor;
            b.transform.position = pos;
            b.transform.forward = Random.insideUnitSphere;
        }
    }

    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        //foreach(var boid in boidArray)
        //{
        //    var cohesion = Cohesion.CalcForce(boid, boidArray);
        //    var seperation = Seperation.CalcForce(boid,boidArray);
        //    var alignment = Alignment.CalcForce(boid, boidArray);
        //    var bounding = Bounding.CalcForce(boid, boundingFactor, maxBoundingDist, minBoundingDist, cubeSize);

        //    boid.velocity += (cohesion * cohesionWeight*Random.Range(0.5f,1.0f)) + 
        //        (alignment * alignmentWeight * Random.Range(0.5f, 1.0f)) +
        //        (seperation * seperationWeight * Random.Range(0.5f, 1.0f)) + bounding;

        //    boid.speed = Mathf.Sqrt((boid.velocity.x * boid.velocity.x) + (boid.velocity.y * boid.velocity.y) + (boid.velocity.z * boid.velocity.z));

        //    if (boid.speed < minSpeed)
        //    {
        //        boid.velocity = (boid.velocity / boid.speed) * minSpeed;
        //    }
        //    if (boid.speed > minSpeed)
        //    {
        //        boid.velocity = (boid.velocity / boid.speed) * maxSpeed;
        //    }
        //    //boid.transform.position += boid.velocity * Time.deltaTime;
        //}
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(0.0f, 1.0f, 0.6f, 0.4f);
        //Gizmos.DrawCube(transform.position, new Vector3(lengthX, lengthY, lengthZ));
    }
        
}
