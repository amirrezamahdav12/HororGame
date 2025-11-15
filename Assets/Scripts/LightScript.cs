using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject flashlight_ground , inition , flashlight_player;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inition.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                flashlight_ground.SetActive(false);
                inition.SetActive(false);
                flashlight_player.SetActive(true);
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
