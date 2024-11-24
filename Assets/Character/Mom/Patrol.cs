using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
    public NavMeshAgent agent;
    public Animator animator;
    public PatrolPoints[] patrolPoints;

    private int currentPatrolIndex = 0;
    private bool isMoving = false;
    public GameObject storyUI;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        storyUI.SetActive(false);
        MoveToCurrentPatrolPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !agent.isStopped)
        {
            if (!isMoving)
            {
                StartCoroutine(WaitAndPerformAction());
            }
        }
        else
        {
            float speed = agent.velocity.magnitude;
            isMoving = speed > 0.1f;
            animator.SetBool("IsMoving", isMoving);
        }
    }

    private IEnumerator WaitAndPerformAction()
    {
        agent.isStopped = true;
        // Set NPC rotation to match the current patrol point's rotation
        Transform currentPatrolPoint = patrolPoints[currentPatrolIndex].transform;
        transform.rotation = currentPatrolPoint.rotation;
        yield return new WaitForSeconds(0.5f);

        bool shouldSit = patrolPoints[currentPatrolIndex].shouldSit;
        bool shouldBeAngry = patrolPoints[currentPatrolIndex].isAngry;
        float waitTime = patrolPoints[currentPatrolIndex].waitingTime;

        Debug.Log("Arrived at patrol point " + currentPatrolIndex + ": Sitting - " + shouldSit + ", Angry - " + shouldBeAngry);

        animator.SetBool("IsSitting", shouldSit);
        animator.SetBool("IsAngry", shouldBeAngry);

        if (shouldBeAngry)
        {
            storyUI.SetActive(true);
        }

        yield return new WaitForSeconds(waitTime);

        animator.SetBool("IsSitting", false);
        animator.SetBool("IsAngry", false);

        // Move to the next patrol point
        agent.isStopped = false;
        IncrementPatrolIndex();
        MoveToCurrentPatrolPoint();
    }

    void MoveToCurrentPatrolPoint()
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
    }

    void IncrementPatrolIndex()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 10 + transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position; 
    }

    private void OnStep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }
}

