using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRot, yRot, zRot); 
    }
}
