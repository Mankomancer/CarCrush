using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_colider_rotation_fix : MonoBehaviour
{
    private RandomMovement parentScript;

    private void Awake()
    {
       parentScript = GetComponentInParent<RandomMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentScript?.HandleTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.CarExitFromMarket(other);
    }
}
