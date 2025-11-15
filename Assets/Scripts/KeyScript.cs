using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject inition, key , sign1 , sign2 , SoundTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inition.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                key.SetActive(false);
                Door.keyfound = true;
                inition.SetActive(false);
                sign1.SetActive(true);
                sign2.SetActive(true);
                SoundTrigger.SetActive(true);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inition.SetActive(false);
        }
    }
}
