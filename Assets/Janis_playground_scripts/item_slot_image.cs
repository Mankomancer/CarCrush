using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_slot_image : MonoBehaviour
{
    [SerializeField]private Image itemSlot;
     
    [SerializeField] private Sprite[] representative_images;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       SlotImageChanger();
    }

    private void SlotImageChanger()
    {
        if (ScoreManager.ItemRecall()==null)
        {
            itemSlot.sprite = null;
            itemSlot.color = Color.clear;
            return;
        }
            itemSlot.color = Color.white;
        

        if (ScoreManager.ItemRecall().tag=="Oil")
        {
            itemSlot.sprite = representative_images[1];
        }else
        if (ScoreManager.ItemRecall().tag=="Auto")
        {
            itemSlot.sprite = representative_images[0];
        }else
        if (ScoreManager.ItemRecall().tag=="Cone")
        {
            itemSlot.sprite = representative_images[2];
        }
    }
}
