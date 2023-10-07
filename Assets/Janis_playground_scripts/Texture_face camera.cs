using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture_facecamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam; // assign the camera in the inspector
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);    
//transform.Rotate(180, 0, 0);
    }
}
