using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beaconScript : MonoBehaviour
{

    public GameObject thelight;
    public bool lightspin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lightspin == true)
        {
            thelight.transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            
            lightspin = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {

            lightspin = false;
        }
    }

}
