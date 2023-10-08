using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No_rotation : MonoBehaviour
{
    // The initial rotation of the child object
    private Quaternion start_position;

// Start is called before the first frame update
    void Start()
    {
        start_position = transform.rotation;
    }

// Update is called once per frame
    void Update()
    {
       
        transform.rotation = start_position;
    }
}
