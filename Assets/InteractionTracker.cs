using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class InteractionTracker : MonoBehaviour
{

    public int interactionState;
    public AudioSource voiceline3;

    // Start is called before the first frame update
    void Start()
    {   
        interactionState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionState <= 5) { 
            voiceline3.Play();
            //end game logic here
        }
    }

    void addInteractionState() { 
        interactionState++;
    }
}
