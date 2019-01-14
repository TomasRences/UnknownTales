using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : CharacterAnimator
{
    Transform target;
    NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathComplete())
        {
            StopRunning();
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
            FacetTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
		StartRunning();
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    void FacetTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRot=Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
		transform.rotation=Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime*5f);
    }
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

}
