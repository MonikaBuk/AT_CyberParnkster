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
    public GameObject screenUI;
    private AudioSource audioMy;

    private void Start()
    {
        audioMy = gameObject.GetComponent<AudioSource>();
    }

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
        audioMy.Play();
        navMeshAgent.isStopped = true;
        screenUI.SetActive(false);
        UI.SetActive(true);
        yield return new WaitForSeconds(animationDuration);


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
}
