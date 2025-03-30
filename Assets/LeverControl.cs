using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotation : MonoBehaviour
{
    public Transform pivotPoint; // Reference to the pivot GameObject
    public float rotationSpeed = 100f; // Rotation speed for the lever
    

    void Update()
    {
        Quaternion theRot = transform.rotation;
        float rot = theRot.x;
        //Debug.Log(rot);

        // Rotate the lever around the pivot point (Y-axis rotation)
        if (Input.GetKey("e") || OVRInput.Get(OVRInput.RawButton.A)) // Rotate clockwise when E key is held
        {
            if (rot < -0.1)
            {
                pivotPoint.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey("q") || OVRInput.Get(OVRInput.RawButton.B)) // Rotate counterclockwise when Q key is held
        {
            
            if (rot > -0.99)
            {
                pivotPoint.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
            }
        }
    }
}

//calculating vector from users hand to origin point of handle 