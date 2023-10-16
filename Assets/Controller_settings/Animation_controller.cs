using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_controller : MonoBehaviour
{
    public Transform playerTransform;
    private Vector2 playerPreviousPositionX;
    private Vector2 playerCurrentPositionX;
    private bool state;
    private bool walk;
    
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
       
      /*  if (IsWalking()&&!ScoreManager.itemSlot)
        {
            ChangeState(1);
        }
        if (IsWalking()&&ScoreManager.itemSlot)
        {
            ChangeState(2);
        }

        if (!IsWalking())
        {
            ChangeState(0);
        }*/
      walk = IsWalking();
      if (ScoreManager.itemSlot != null)
      {
           state = true; 
      }else
      {
           state = false;
      }
      
      switch (walk, state)
      {
          case (true, false):
              ChangeState(1);
              break;
          case (true, true):
              ChangeState(2);
              break;
          case (false, _):
              ChangeState(0);
              break;
      }
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
