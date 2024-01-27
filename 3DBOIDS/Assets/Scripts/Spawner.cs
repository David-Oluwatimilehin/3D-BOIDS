using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BoidSettings
{
    public int velocity = 16;
    public int neighbourDist = 10;
    public int nearDist = 4;
    public int attractPushDist = 5;


    public float velMatching = 1.5f;
    public float flockCentering = 1.0f;
    public float nearAvoid = 2.0f;
    public float attractPull = 1.0f;
    public float attractPush = 15.0f;

    public float velEasing = 0.03f;

}

public class Spawner : MonoBehaviour
{
    static public BoidSettings SETTINGS;
    static public List<Boid> BOIDS;

    [Header("Incscribed: Settings For Boids")]
    public GameObject boidPrefab;
    public Transform boidAnchor;
    public int numberOfBoids = 20;

    public float spawnRadius=50.0f;
    public float spawnDelay = 0.1f;


    public BoidSettings boidSettings;

    public void InitializeBoids()
    {
        GameObject gameObject = Instantiate(boidPrefab);
        gameObject.transform.position = Random.insideUnitSphere * spawnRadius;
        Boid b = gameObject.GetComponent<Boid>();
        b.transform.SetParent(boidAnchor);
        BOIDS.Add(b);
        if (BOIDS.Count < numberOfBoids)
        {
            Invoke("InitializeBoids", spawnDelay);
        }

    }
    private void Awake()
    {
        SETTINGS = boidSettings;
        BOIDS = new List<Boid>();
        InitializeBoids();

    }
}
