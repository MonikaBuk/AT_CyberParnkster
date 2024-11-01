using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public PatrolPoints[] patrolPoints;
    public float patrolWaitTime = 2f; // Time to wait at each patrol point

    private NavMeshAgent agent;
    private Animator animator;
    private int currentPatrolIndex;
    private bool isMoving;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentPatrolIndex = 0;

        MoveToNextPatrolPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !agent.isStopped)
        {
            if (!isMoving)
            {
                StartCoroutine(WaitAndMove());
            }
        }

        // Update animation based on speed
        float speed = agent.velocity.magnitude;
        isMoving = speed > 0.1f;
        animator.SetBool("IsMoving", isMoving);
    }

    private IEnumerator WaitAndMove()
    {

        agent.isStopped = true;
        Transform targetPoint = patrolPoints[currentPatrolIndex].transform;
        transform.rotation = targetPoint.rotation;
        bool shouldSit = patrolPoints[currentPatrolIndex].shouldSit;
        Debug.Log("Should sit at point " + currentPatrolIndex + ": " + shouldSit);
        animator.SetBool("IsSitting", !shouldSit);
        yield return new WaitForSeconds(patrolPoints[currentPatrolIndex].wairingTime);
        animator.SetBool("IsSitting", false);
        agent.isStopped = false;

        MoveToNextPatrolPoint();
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex].transform;
        NavMeshPath path = new NavMeshPath();

        if (agent.CalculatePath(targetPoint.position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(targetPoint.position);
        }
        else
        {
            Debug.LogWarning("Invalid path to " + targetPoint.name + ", switching to random point.");
            Vector3 randomPoint = GetRandomNavMeshPoint();
            agent.SetDestination(randomPoint);
        }

        // Update to the next patrol point index
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 10;
        randomPoint += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }
}