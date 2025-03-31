using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrig : MonoBehaviour

{

    public bool Rightfrequency;
    public Light lightSource;
    public AudioSource numStation, radioFreq;

    // Start is called before the first frame update
    void Start()
    {
        Rightfrequency = false;
        lightSource.enabled = false;
        radioFreq.Play();
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
            numStation.Play();
            radioFreq.Stop();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            Rightfrequency = false;
            lightSource.enabled = false;
            numStation.Stop();
            radioFreq.Play();
        }
    }
}
