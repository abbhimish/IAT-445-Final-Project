using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keypressScript : MonoBehaviour
{

    public bool TerminalUlocked = false;

    public GameObject bigLever;
    public Rigidbody bigleverRB;

    public string codeslot1 = "";
    public string codeslot2 = "";
    public string codeslot3 = "";
    public string codeslot4 = "";

    public TextMesh codeDisplay;

    public Light Beaconleverlivelight;

    // Start is called before the first frame update
    void Start()
    {
        codeDisplay.text = "";
        bigleverRB.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void testCode()
    {
        if (codeslot1 == "1" && codeslot2 == "5" && codeslot3 == "3" && codeslot4 == "2")
        {
            TerminalUlocked = true;
            codeDisplay.color = Color.green;
            bigleverRB.detectCollisions = true;
            Beaconleverlivelight.color = Color.green;
        }
        else {
            codeDisplay.color = Color.red;
            Invoke("updateText", 2);
        }
        
        
    }

    void updateText()
    {
        codeslot1 = "";
        codeslot2 = "";
        codeslot3 = "";
        codeslot4 = "";
        codeDisplay.text = codeslot1 + codeslot2 + codeslot3 + codeslot4;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Key1"))
        {
            codeDisplay.color = Color.white;
            if (codeslot1 == "")
            {
                codeslot1 = "1";
            }
            else if (codeslot2 == "")
            {
                codeslot2 = "1";
            }
            else if (codeslot3 == "")
            {
                codeslot3 = "1";
            }
            else if (codeslot4 == "")
            {
                codeslot4 = "1";
                testCode();
            }

            codeDisplay.text = codeslot1+ codeslot2+ codeslot3 + codeslot4;

        }

        if (collision.gameObject.CompareTag("Key2"))
        {
            codeDisplay.color = Color.white;
            if (codeslot1 == "")
            {
                codeslot1 = "2";
            }
            else if (codeslot2 == "")
            {
                codeslot2 = "2";
            }
            else if (codeslot3 == "")
            {
                codeslot3 = "2";
            }
            else if (codeslot4 == "")
            {
                codeslot4 = "2";
                testCode();
            }

            codeDisplay.text = codeslot1 + codeslot2 + codeslot3 + codeslot4;

        }

        if (collision.gameObject.CompareTag("Key3"))
        {
            codeDisplay.color = Color.white;
            if (codeslot1 == "")
            {
                codeslot1 = "3";
            }
            else if (codeslot2 == "")
            {
                codeslot2 = "3";
            }
            else if (codeslot3 == "")
            {
                codeslot3 = "3";
            }
            else if (codeslot4 == "")
            {
                codeslot4 = "3";
                testCode();
            }

            codeDisplay.text = codeslot1 + codeslot2 + codeslot3 + codeslot4;

        }

        if (collision.gameObject.CompareTag("Key4"))
        {
            codeDisplay.color = Color.white;
            if (codeslot1 == "")
            {
                codeslot1 = "4";
            }
            else if (codeslot2 == "")
            {
                codeslot2 = "4";
            }
            else if (codeslot3 == "")
            {
                codeslot3 = "4";
            }
            else if (codeslot4 == "")
            {
                codeslot4 = "4";
                testCode();
            }

            codeDisplay.text = codeslot1 + codeslot2 + codeslot3 + codeslot4;

        }

        if (collision.gameObject.CompareTag("Key5"))
        {
            codeDisplay.color = Color.white;
            if (codeslot1 == "")
            {
                codeslot1 = "5";
            }
            else if (codeslot2 == "")
            {
                codeslot2 = "5";
            }
            else if (codeslot3 == "")
            {
                codeslot3 = "5";
            }
            else if (codeslot4 == "")
            {
                codeslot4 = "5";
                testCode();
            }

            codeDisplay.text = codeslot1 + codeslot2 + codeslot3 + codeslot4;

        }

    }

}
