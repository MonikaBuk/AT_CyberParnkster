using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPointToAnrgy : MonoBehaviour
{
    public PatrolPoints point;

    private void OnEnable()
    {
        point.isAngry = true;
    }
}
