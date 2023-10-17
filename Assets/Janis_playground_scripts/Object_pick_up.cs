using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pick_up : MonoBehaviour
{
    private Input_controlls controlls;
    private bool Action_button;
    private bool boughtCone = false;
    private int conePrice = 10;
    [SerializeField] private GameObject item_hold_spot;
    [SerializeField] public GameObject shopObject;

    private void Awake()
    {
        Action_button = false;
        controlls = new Input_controlls();
        controlls.Gameplay.Action.performed += ctx => Action_set();
        controlls.Gameplay.Action.canceled += ctx =>Action_Release();
    }

    public void HandleTrigger(Transform other)
    {
        if (Action_button && other && ScoreManager.itemSlot == null && other.tag!="Shop")
        {
            if (other.tag=="Cone" && !boughtCone && shopObject.GetComponent<Shop_script>().money>=conePrice){ //buying cone
                shopObject.GetComponent<Shop_script>().money-=conePrice;
                boughtCone = true;
            }
            if (other.tag!="Cone" || boughtCone){   //in case player already bought cone, but dropped it somewhere else
                ScoreManager.InsertItem(other.gameObject);
                ScoreManager.ItemRecall()?.transform.SetParent(item_hold_spot.transform);
                ScoreManager.ItemRecall().transform.localPosition = new Vector3(0,0,0.1f);
            }
        }
    }

    void Action_Release()
    {
        Action_button = false;
    }
    void Action_set()
    {
        if (ScoreManager.itemSlot == null)
        {
            Action_button = true;
            return;
        }
        ReleaseItem();  //maybe, need to decrese height of item released, otherwise they tend to hover
    }

    void ReleaseItem()
    {
        ScoreManager.ItemRecall().transform.parent = null;
        ScoreManager.DropItem();
    }
    private void OnEnable()
    {
        controlls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controlls.Gameplay.Disable();
    }
}
