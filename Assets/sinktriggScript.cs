using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinktriggScript : MonoBehaviour
{

    public ParticleSystem sinkwater;
    public GameObject ocean;
    public bool seagoingdown = false;

    // Start is called before the first frame update
    void Start()
    {
        sinkwater.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (seagoingdown == true)
        {
            ocean.transform.Translate(Vector3.down * 0.1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            sinkwater.Play();
            seagoingdown = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            sinkwater.Stop();
            seagoingdown = false;
        }
    }
}
