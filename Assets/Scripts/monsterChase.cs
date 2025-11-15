using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterChase : MonoBehaviour
{
    public Rigidbody monsRigid;
    public Transform monsTrans, playTrans;
    public float monSpeed = 5f; // Changed to float for more precise speed control
    public bool debug = false; // Changed to lowercase 'debug' to follow C# naming conventions

    void Update() // Changed to Update for consistent LookAt behavior
    {
        if (playTrans != null && monsTrans != null) // Added null checks
        {
            monsTrans.LookAt(playTrans);
        }
        else
        {
            if (debug)
            {
                Debug.LogWarning("playTrans or monsTrans is null");
            }

        }
    }

    void FixedUpdate()
    {
        if (monsRigid != null && playTrans != null) //Added null checks
        {
            Vector3 direction = (playTrans.position - monsTrans.position).normalized;
            monsRigid.velocity = direction * monSpeed;
        }
        else
        {
            if (debug)
            {
                if(monsRigid == null) Debug.LogWarning("monsRigid is null");
                if(playTrans == null) Debug.LogWarning("playTrans is null");
            }
        }

    }
}