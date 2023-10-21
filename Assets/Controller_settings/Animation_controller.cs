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
    private bool carMounting = false;
    
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
       

      walk = IsWalking();
      if (ScoreManager.itemSlot!=null)
      {
          state = true; 
          if (ScoreManager.itemSlot.tag == "Auto")
          {
              carMounting = true;
          }
      }else
      {
           state = false;
      }

      if (ScoreManager.itemSlot != null)
      {
          if (ScoreManager.itemSlot?.tag !="Auto")
          {
              carMounting = false;
          }
      }
      if (ScoreManager.itemSlot is null )
      {
          carMounting = false;
      }
      
      
      switch (walk, state,carMounting)
      {
          case (true, false,false):
              ChangeState(1);
              break;
          case (true, true,false):
              ChangeState(2);
              break;
          case (false, _,false):
              ChangeState(0);
              break;
          case ( _, _,true):
              ChangeState(3);
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
