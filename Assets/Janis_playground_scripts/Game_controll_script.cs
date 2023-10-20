using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controll_script : MonoBehaviour
{
    [SerializeField] private float doomsDayTimerStarTime;
    [SerializeField] private int barrelPrice;
    [SerializeField] private UI_handler uiText;
    [SerializeField] private Shop_script shopScript;
    // Start is called before the first frame update
    void Start()
    {
        uiText.DoomsDayTimerSet(doomsDayTimerStarTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
