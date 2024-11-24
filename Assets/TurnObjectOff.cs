using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObjectOff : MonoBehaviour
{
        public float delay = 3.0f;   

        void Start()
        {
            StartCoroutine(HideTextAfterDelay());
        }

        IEnumerator HideTextAfterDelay()
        {
            yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        }
    
}
