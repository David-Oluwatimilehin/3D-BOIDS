using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAttractor : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Attractor.position);
    }
}
