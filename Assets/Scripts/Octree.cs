using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    private Node root;
    private int maxBoidsPerNode;
    private float minNodeSize;

    public Octree(Bounds bounds, int maxBoidsPerNode, float minNodeSize)
    {
        root= new Node(bounds,maxBoidsPerNode, minNodeSize);
        this.maxBoidsPerNode=maxBoidsPerNode;
        this.minNodeSize=minNodeSize;
    }

    public void Insert(Boid boid)
    {
        root.Insert(boid);
    }

    public void Clear()
    {
        root = new Node(root.bounds, maxBoidsPerNode, minNodeSize);
    }
    
    public List<Boid> Query(Vector3 pos, float radius)
    {
        Bounds queryBounds = new Bounds(pos, Vector3.one * radius * 2);
        return root.Query(queryBounds);
    }
}
