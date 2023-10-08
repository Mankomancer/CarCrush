using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_rotate : MonoBehaviour
{
    // Declare a public variable for the rotation speed
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the z axis by speed * Time.deltaTime degrees per frame
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
