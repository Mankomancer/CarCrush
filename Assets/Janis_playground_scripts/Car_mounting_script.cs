using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_mounting_script : MonoBehaviour
{
    [SerializeField] private RandomMovement carMovement;
    [SerializeField] private GameObject CarSprite;
    [SerializeField] private Sprite_flip_check_direction flip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsCareMounted();
    }

    private void IsCareMounted()
    {
        if (ScoreManager.itemSlot==gameObject && carMovement.enabled)
        {
            carMovement.enabled = false;
            flip.enabled = false;
        }

        if (ScoreManager.itemSlot != gameObject && !carMovement.enabled)
        {
            carMovement.enabled = true;
            flip.enabled = true;
        }

        if (ScoreManager.itemSlot == gameObject)
        {
            CarFacingDirection();
        }
    }

    void CarFacingDirection()
    {
        if (ScoreManager.playerIsFacingRight)
        {
            CarSprite.transform.localScale = new Vector3(Mathf.Abs(CarSprite.transform.localScale.x),CarSprite.transform.localScale.y,CarSprite.transform.localScale.z);
        }else CarSprite.transform.localScale = new Vector3(Mathf.Abs(CarSprite.transform.localScale.x)*-1,CarSprite.transform.localScale.y,CarSprite.transform.localScale.z);
    }
}
