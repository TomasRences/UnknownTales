using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterAnimator
{
    public float lookRadius = 10f;
    NavMeshAgent agent;
    Transform target;

    CharacterCombat combat;
    CharacterStats myStats;

    // Use this for initialization
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        target = PlayerManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.IsDied) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();

                if (targetStats != null)
                {
                    StartAttacking();
                    FaceTarget();
                    StopRunning();
                    combat.Attack(targetStats);
                }
            }
            else
            {
                StopAttacking();
                StartRunning();
            }
        }

        if (pathComplete())
        {
            StopRunning();
        }
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

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
