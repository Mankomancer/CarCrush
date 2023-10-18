using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_colider_rotation_fix : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Forward the trigger event to the parent script
        // Let's say the parent script is called Object_pick_up
        RandomMovement parentScript = GetComponentInParent<RandomMovement>();
        // Invoke the parent script's method for handling the trigger event
        parentScript?.HandleTrigger(other);
    }
}
