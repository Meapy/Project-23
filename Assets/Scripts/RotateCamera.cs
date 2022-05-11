using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [Range(-100,100)]
    public float rotationSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        //if y rotation axis greater than 60, rotate other way
        
        if(transform.rotation.y > 0.60)
        {
            rotationSpeed = -2f;
        }
        else if(transform.rotation.y < 0)
        {
            rotationSpeed = 2f;
        }
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}