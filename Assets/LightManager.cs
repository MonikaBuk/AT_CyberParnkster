using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToControl;
    [SerializeField] private bool isStartingRoom = false;

    private void Start()
    {
        if(!isStartingRoom)
        {
            ObjectToControl.gameObject.SetActive(false);
        }
        else
        {
            ObjectToControl.gameObject.SetActive(true);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectToControl.gameObject.SetActive(true);
        }

    }


    // Optionally, you can also detect when an object exits the collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectToControl.gameObject.SetActive(false);
        }
    }
}