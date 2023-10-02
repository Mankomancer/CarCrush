using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float playerSpeed = 30f;
    public float jumpForce = 5f;
    private float horizonatInput;
    private float forwardInput;
    private Rigidbody playerRb;
    public bool isOnGround = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        horizonatInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //move player character
        transform.Translate(Vector3.forward * Time.deltaTime * forwardInput * playerSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizonatInput * playerSpeed);

        //jump for player * Time.deltaTime
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }
    }
}
