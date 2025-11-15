using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    // variables
    public Transform target;
    private NavMeshAgent agent;
    public Animator Anim;

    // methods
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //giving component to object
    }

    private void Update() // updating data after the fisrt frame
    {
        if (target != null) // checking if target is not null
        {
            agent.SetDestination(target.position); // giving agent new destiniation
            IsMoving();
        }
    }

    public void IsMoving() // checking if the character is moving or not
    {
        if (Anim == null) return; // Prevents NullReferenceException

        bool isMoving = agent.velocity.magnitude > 0.1f;
        Anim.SetBool("IsMoving", isMoving);
    }

    public void StopMoving() // checking stop moving
    {
        if (Anim == null) return;

        bool isIdle = agent.velocity.magnitude <= 0.1f;
        Anim.SetBool("Idle", isIdle);
    }

    public void CheckingCorners() // checking corners for new destinations
    {
        if (Anim == null) return;

        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Anim.SetBool("CheckingCorners", true);
        }
        else
        {
            Anim.SetBool("CheckingCorners", false);
        }
    }
}