using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleRotationScript : MonoBehaviour
{
    public Transform pivotPoint; // Reference to the pivot GameObject
    public float rotationSpeed = 100f; // Rotation speed for the lever
    public ParticleSystem sinkwater;
    public GameObject Sealevel;

    void Update()
    {
        Quaternion theRot = transform.rotation;
        float rot = theRot.y;
        //Debug.Log(rot);

        if (rot > 0.5)
        {
            sinkwater.Play();
            Sealevel.transform.Translate(Vector3.down * 0.1f * Time.deltaTime);
        }

        if (rot < 0.2)
        {
            sinkwater.Stop();
        }

        // Rotate the lever around the pivot point (Y-axis rotation)
        if (Input.GetKey("u") || OVRInput.Get(OVRInput.RawButton.A)) // Rotate clockwise when E key is held
        {
            if (rot < 0.7)
            {
                pivotPoint.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                
            }
        }

        if (Input.GetKey("i") || OVRInput.Get(OVRInput.RawButton.B)) // Rotate counterclockwise when Q key is held
        {

            if (rot > 0)
            {
                pivotPoint.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
