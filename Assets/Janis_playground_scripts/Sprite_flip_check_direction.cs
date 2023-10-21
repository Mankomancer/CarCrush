using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_flip_check_direction : MonoBehaviour
{
    private Transform playerTransform;
    private float playerPreviousPositionX;
    private float playerCurrentPositionX;
    private float x_scale_positive;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        x_scale_positive = Mathf.Abs(playerTransform.localScale.x);
        playerPreviousPositionX = playerTransform.position.x;
        playerCurrentPositionX = playerTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentPositionX = playerTransform.position.x;
        if (playerCurrentPositionX > playerPreviousPositionX)
        {
            playerTransform.localScale = new Vector3(x_scale_positive, playerTransform.localScale.y, playerTransform.localScale.z);
            playerPreviousPositionX = playerCurrentPositionX;
        }
        else if (playerCurrentPositionX < playerPreviousPositionX)
        {
            playerTransform.localScale = new Vector3(-x_scale_positive, playerTransform.localScale.y, playerTransform.localScale.z);
            playerPreviousPositionX = playerCurrentPositionX;
        }
    }

    public bool FlipDirection()
    {
        if (playerTransform.localScale.x > 0)
        {
            return true;
        }
        else return false;
    }
}
