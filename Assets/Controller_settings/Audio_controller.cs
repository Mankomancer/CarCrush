using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Audio_controller : MonoBehaviour
{
    [SerializeField] private AudioSource AutoCollisions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.carColsisionSound)
        {
            AutoCollisions.Play();
            ScoreManager.carColsisionSound = false;
        }
    }
}
