using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controll_script : MonoBehaviour
{
    [SerializeField] private float doomsDayTimerStarTime;
    [SerializeField] private int barrelPrice;
    [SerializeField] private UI_handler uiText;
    [SerializeField] private Market_csript Market;
    [SerializeField] private float coneCarDestroyTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.CarDestroyTime = coneCarDestroyTimer;
        uiText.DoomsDayTimerSet(doomsDayTimerStarTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
