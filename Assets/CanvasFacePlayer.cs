using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    public Camera vrCamera; // Assign the VR Camera here

    void Update()
    {
        // Make sure the UI canvas always faces the camera
        if (vrCamera != null)
        {
            transform.LookAt(vrCamera.transform);
        }
    }
}
