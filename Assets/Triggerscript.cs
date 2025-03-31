using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerscript : MonoBehaviour
{

    public bool isTrig = false;
    public Light lightSource;
    public ParticleSystem smoke1;
    public ParticleSystem sparks1;

    public ParticleSystem firepart1;
    public ParticleSystem firepart2;
    public ParticleSystem firepart3;
    public ParticleSystem firepart4;

    public AudioSource genAmbAudio, genTurnOn, genTurnOff, voiceline2;

    bool voiceLinePlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        isTrig = false;
        lightSource.enabled = false;
        smoke1.Stop();
        sparks1.Stop();
        stopfire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            isTrig = true;
            lightSource.enabled = true;
            smoke1.Play();
            sparks1.Play();
            Invoke("startfire", 5);
            genAmbAudio.Play();
            genTurnOn.Play();

            if (voiceLinePlayed == false) { 
            voiceline2.Play();
            voiceLinePlayed = true;
        }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            isTrig = false;
            lightSource.enabled = false;
            smoke1.Stop();
            sparks1.Stop();
            genAmbAudio.Stop();
            genTurnOff.Play();
        }
    }

    void startfire()
    {
        firepart1.Play();
        firepart2.Play();
        firepart3.Play();
        firepart4.Play();
    }

    void stopfire()
    {
        firepart1.Stop();
        firepart2.Stop();
        firepart3.Stop();
        firepart4.Stop();
    }

}
