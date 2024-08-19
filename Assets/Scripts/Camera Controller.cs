using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float mainSpeed = 200.0f;
    float shiftAdd = 250.0f;
    float maxShift = 1000.0f;
    float camSens = 0.25f;

    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun = 1.0f;

    
    void Update()
    {
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPos = transform.position;
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(p);
                newPos.x = transform.position.x;
                newPos.z = transform.position.z;
                transform.position = newPos;
            }
            else
            {
                transform.Translate(p);
            }
        }
    }
    private Vector3 GetBaseInput()
    {
        Vector3 pointerVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            pointerVelocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pointerVelocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            pointerVelocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pointerVelocity += new Vector3(1, 0, 0);
        }
        return pointerVelocity;
    }
}
