using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconLeverScript : MonoBehaviour
{
    public Transform pivotPoint; // Reference to the pivot GameObject
    public float rotationSpeed = 100f; // Rotation speed for the lever
    public GameObject bulbHinge;


    void Update()
    {
        Quaternion theRot = transform.rotation;
        float rot = theRot.x;
        //Debug.Log(rot);

        if (rot > -0.1) {
            bulbHinge.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }

        // Rotate the lever around the pivot point (Y-axis rotation)
        if (Input.GetKey("o") || OVRInput.Get(OVRInput.RawButton.A)) // Rotate clockwise when E key is held
        {
            if (rot > -0.99)
            {
                pivotPoint.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey("p") || OVRInput.Get(OVRInput.RawButton.B)) // Rotate counterclockwise when Q key is held
        {

            if (rot < -0.01)
            {
                pivotPoint.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
