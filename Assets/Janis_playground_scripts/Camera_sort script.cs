using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_sortscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 customAxis = new Vector3(0, 0, 1);
    void Start()
    {
        // Get the camera component attached to this game object
        Camera camera = GetComponent<Camera>();

        // Set the transparency sort mode to custom axis
        camera.transparencySortMode = TransparencySortMode.CustomAxis;

        // Set the transparency sort axis to the custom axis
        camera.transparencySortAxis = customAxis;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
