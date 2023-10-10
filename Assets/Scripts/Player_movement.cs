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

   
    // Start is called before the first frame update
    void Start()
    {

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


}
