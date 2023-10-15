using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_controller : MonoBehaviour
{
    public Transform playerTransform;
    private Vector2 playerPreviousPositionX;
    private Vector2 playerCurrentPositionX;
    
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerPreviousPositionX = new Vector2(playerTransform.position.x,playerTransform.position.z);
        playerCurrentPositionX = new Vector2(playerTransform.position.x,playerTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWalking())
        {
            ChangeState(1);
        }else ChangeState(0);
    }

    void ChangeState(int state)
    {
        animator.SetInteger("Controller",state);
    }

    bool IsWalking()
    {
        playerCurrentPositionX = new Vector2(playerTransform.position.x,playerTransform.position.z);
        if (playerCurrentPositionX == playerPreviousPositionX)
        {
            return false;
        }
        if (playerCurrentPositionX != playerPreviousPositionX)
        {
            playerPreviousPositionX = playerCurrentPositionX;
            return true;
        }
        return false;
    }
}
