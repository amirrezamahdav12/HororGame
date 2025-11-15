using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorclosed, door_Opened, intext , lockedtext;
    public AudioSource dooropened , Door_closed;
    public bool opened , locked;
    public static bool keyfound;
    
    
    
    private void Start()
    {
        keyfound = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("you have main camera");

            if (locked) // اگر در قفل بود
            {
                Debug.Log("locked is true");
                lockedtext.SetActive(true);
            }
            else // اگر در قفل نبود
            {
                Debug.Log("locked is false");
                intext.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("key is down");
                    doorclosed.SetActive(false);
                    door_Opened.SetActive(true);
                    intext.SetActive(false);
                    StartCoroutine(repeat());
                    opened = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            intext.SetActive(false);
            lockedtext.SetActive(false);
        }
    }

    IEnumerator repeat()
    {
        yield return new WaitForSeconds(1.5f);
        opened = false;
        door_Opened.SetActive(false);
        doorclosed.SetActive(true);
        // closeplay();
    }

    private void Update()
    {
        if (keyfound)
        {
            if(keyfound == true)
            {
                locked = false;
            }
        }
    }
}
