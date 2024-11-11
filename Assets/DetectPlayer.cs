using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{
    public Animator animator; 
    public string animationTriggerName = "PlayAnimation"; 
    public float animationDuration = 2f;
    public NavMeshAgent navMeshAgent;
    public GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayAnimationAndRestart());
        }
    }

    private IEnumerator PlayAnimationAndRestart()
    {
       
        animator.SetBool("IsAngry", true);
        navMeshAgent.isStopped = true;
        UI.SetActive(true);
        yield return new WaitForSeconds(animationDuration);    
        SceneManager.LoadScene("Password");
    
    }
}
