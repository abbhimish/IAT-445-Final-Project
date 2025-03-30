using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrig : MonoBehaviour

{

    public bool Rightfrequency;
    public Light lightSource;

    // Start is called before the first frame update
    void Start()
    {
        Rightfrequency = false;
        lightSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            Rightfrequency = true;
            lightSource.enabled = true;
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            Rightfrequency = false;
            lightSource.enabled = false;
            
        }
    }
}
