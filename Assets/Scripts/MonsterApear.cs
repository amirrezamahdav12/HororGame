using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApear : MonoBehaviour
{
    public GameObject Monster;
    public Collider Collisiton1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Monster.SetActive(true);
            Collisiton1.enabled = false;
        }
    }
}
