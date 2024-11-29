using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreenHandler : MonoBehaviour
{
    
    private bool isPlayerInsideTrigger = false;
    public GameObject computerScreen;
    public bool canBeDisabled = true ;
    private AudioSource audioMy;


    private void Start()
    {
        audioMy = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            if (canBeDisabled)
            {
                isPlayerInsideTrigger = false;
            }
        }
    }

    void Update()
    {
        if (isPlayerInsideTrigger && Input.GetKeyDown(KeyCode.E)) 
        {
            if(computerScreen.activeInHierarchy)
            {
                computerScreen.SetActive(false);
            }
            else
            {
                computerScreen.SetActive(true);
                audioMy.Play();
            }
        }
    }
} 

