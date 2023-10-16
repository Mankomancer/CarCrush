using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float playerSpeed_default = 15f;
    public float playerSpeed = 15f;
    private float horizonatInput;
    private float forwardInput;
    private float boostTime;
    private bool boosting;

    public float minX = -49f;
    public float maxX = 49f;
    public float minZ = -49f;
    public float maxZ = 49f;
    private Vector3 newDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        newDirection = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        horizonatInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        newDirection =new Vector3(horizonatInput,0,forwardInput);
        Movement_With_Limits(newDirection, playerSpeed);
       //move player character
     // transform.Translate(Vector3.forward * Time.deltaTime * forwardInput * playerSpeed);
     // transform.Translate(Vector3.right * Time.deltaTime * horizonatInput * playerSpeed);

        if (boosting){
            boostTime+=Time.deltaTime;
            if (boostTime>5){
                playerSpeed = playerSpeed_default;
                boosting = false;
                boostTime = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Shop"){
            boosting = true;
            playerSpeed = 25f;
        }
    }

    private void Movement_With_Limits(Vector3 direction,float speed)
    {
        if (direction != Vector3.zero)
        {
            // Calculate the displacement vector from the direction and speed
            Vector3 displacement = direction * speed * Time.deltaTime;

            // Add the displacement vector to the current position
            Vector3 newPosition = transform.position + displacement;

            // Clamp the x value of the new position to the min and max values
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            // Use transform.translate to move the player game object to the new position
            transform.Translate(newPosition - transform.position);
        }
    }
}
