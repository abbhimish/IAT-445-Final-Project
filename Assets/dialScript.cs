using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialScript : MonoBehaviour
{
    public Transform observedObject;  
    public Transform objectToRotate;  

    private Quaternion lastObservedRotation;

    void Start()
    {
        if (observedObject == null || objectToRotate == null)
        {
            Debug.LogError("Please assign both observedObject and objectToRotate.");
            return;
        }

        lastObservedRotation = observedObject.rotation;
    }

    void Update()
    {
        
        Quaternion deltaRotation = observedObject.rotation * Quaternion.Inverse(lastObservedRotation);

        
        objectToRotate.rotation *= Quaternion.Slerp(Quaternion.identity, deltaRotation, 0.08f);

        
        lastObservedRotation = observedObject.rotation;
    }
}
