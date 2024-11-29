using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOverTime : MonoBehaviour
{
    public float delay = 1.0f; 

    void OnEnable()
    {
        // Start the coroutine
        StartCoroutine(EnableChildrenOneByOne());
    }

    IEnumerator EnableChildrenOneByOne()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);          
            yield return new WaitForSeconds(delay);   
        }
    }
}
