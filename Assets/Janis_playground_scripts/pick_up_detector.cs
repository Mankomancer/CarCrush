using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick_up_detector : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        // Forward the trigger event to the parent script
        // Let's say the parent script is called Object_pick_up
        Object_pick_up parentScript = GetComponentInParent<Object_pick_up>();
       
        // Invoke the parent script's method for handling the trigger event
         parentScript.HandleTrigger(other.transform);
    }
}
