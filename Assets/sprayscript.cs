using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprayscript : MonoBehaviour
{

    public ParticleSystem foam;

    public ParticleSystem firepart1;
    public ParticleSystem firepart2;
    public ParticleSystem firepart3;
    public ParticleSystem firepart4;

    public bool istrig = false;

    public AudioSource fireExtSound, spraysound;

    // Start is called before the first frame update
    void Start()
    {
        foam.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("firezone"))
        {

            foam.Play();
            Invoke("stopfire", 5);
            istrig = true;
            fireExtSound.Play();
            spraysound.Play();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("firezone"))
        {
            foam.Stop();
            spraysound.Stop();
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
