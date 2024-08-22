using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node
{
    public Bounds bounds;
    public List<Boid> boids;
    public Node[] children;
    private int maxBoids;
    private float minSize;

    public Node(Bounds bounds,int maxBoids, float minSize)
    {
        this.bounds = bounds;
        this.maxBoids = maxBoids;
        this.minSize = minSize;
        
        this.boids = new List<Boid>();
        this.children = null;
    }

    public void Insert(Boid boid)
    {
        if(children!= null)
        {
            int index = GetChildIndex(boid.position);
            children[index].Insert(boid);
            return;
        }

        boids.Add(boid);

        if (boids.Count > maxBoids && bounds.size.x > minSize)
        {
            Debug.Log("We've divided");

            Subdivide();
            for (int i = boids.Count - 1; i >= 0; i--)
            {
                int index = GetChildIndex(boids[i].position);
                children[index].Insert(boids[i]);
                boids.Remove(boids[i]);

            }
        }
    }

    public List<Boid> Query(Bounds queryBounds)
    {
        List<Boid> results = new List<Boid>();

        if (!bounds.Intersects(queryBounds))
        {
            return results;
        }

        if (children == null)
        {
            foreach (var b in boids)
            {
                if (queryBounds.Contains(b.position))
                {
                    results.Add(b);
                }
            }
        }
        else
        {
            foreach (var child in children)
            {
                results.AddRange(child.Query(queryBounds));
            }
        }
        return results;
    
    }

    private int GetChildIndex(Vector3 pos)
    {
        int index = 0;

        if (pos.x >= bounds.center.x) 
            index |= 1;
        if (pos.y >= bounds.center.y)
            index |= 2;
        if (pos.z >= bounds.center.z)
            index |= 4;
        
        return index;
    }

    void Subdivide()
    {
        Vector3 size = bounds.size / 2f;
        Vector3 center = bounds.center;

        children = new Node[8];
        children[0] = new Node(new Bounds(center + new Vector3(-size.x, -size.y, -size.z) / 2f, size), maxBoids, minSize);
        children[1] = new Node(new Bounds(center + new Vector3(size.x, -size.y, -size.z) / 2f, size), maxBoids, minSize);
        children[2] = new Node(new Bounds(center + new Vector3(-size.x, size.y, -size.z) / 2f, size), maxBoids, minSize);
        children[3] = new Node(new Bounds(center + new Vector3(size.x, size.y, -size.z) / 2f, size), maxBoids, minSize);
        children[4] = new Node(new Bounds(center + new Vector3(-size.x, -size.y, size.z) / 2f, size), maxBoids, minSize);
        children[5] = new Node(new Bounds(center + new Vector3(size.x, -size.y, size.z) / 2f, size), maxBoids, minSize);
        children[6] = new Node(new Bounds(center + new Vector3(-size.x, size.y, size.z) / 2f, size), maxBoids, minSize);
        children[7] = new Node(new Bounds(center + new Vector3(size.x, size.y, size.z) / 2f, size), maxBoids, minSize);
    }
}
