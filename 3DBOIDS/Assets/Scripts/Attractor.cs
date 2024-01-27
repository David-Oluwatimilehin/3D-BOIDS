using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    static public Vector3 position = Vector3.zero;

    public Vector3 range = new Vector3 (40, 0, 40);
    public Vector3 phase = new Vector3(0.5f, 0.4f, 0.1f);

    void FixedUpdate()
    {
        Vector3 temPos = transform.position;
        temPos.x = Mathf.Sin(phase.x * Time.deltaTime) * range.x;
        temPos.y = Mathf.Sin(phase.y * Time.deltaTime) * range.y;
        temPos.z = Mathf.Sin(phase.z * Time.deltaTime) * range.z;
        transform.position = temPos;
        position = temPos;
    }

    
}
