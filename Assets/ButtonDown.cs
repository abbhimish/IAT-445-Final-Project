using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    public GameObject button;
    Vector3 buttonPos;
    float buttonYPos;
    float ogbuttonYPos;


    // Start is called before the first frame update
    void Start()
    {
        buttonPos = button.transform.position;
        buttonYPos = buttonPos.y;
        ogbuttonYPos = buttonPos.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r") || OVRInput.Get(OVRInput.RawButton.A)) 
        {
            button.transform.Translate(Vector3.down*0.01f);
        }

    }
}
