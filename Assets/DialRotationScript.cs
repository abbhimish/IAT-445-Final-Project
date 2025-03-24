using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotationScript : MonoBehaviour
{

    public Transform pivotPoint;
    public float rotationSpeed = 100f;
    public ParticleSystem birds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("t") || OVRInput.Get(OVRInput.RawButton.X)) // Rotate clockwise when E key is held
        { 
            pivotPoint.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            birds.transform.Rotate(Vector3.up * 10 * Time.deltaTime);
        }

        if (Input.GetKey("y") || OVRInput.Get(OVRInput.RawButton.Y)) // Rotate clockwise when E key is held
        {
            pivotPoint.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            birds.transform.Rotate(Vector3.down * 10 * Time.deltaTime);
        }

    }
}
