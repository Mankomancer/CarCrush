using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controll_script : MonoBehaviour
{
    [SerializeField] private float doomsDayTimerStarTime;
    [SerializeField] private float coneCarDestroyTimer = 5f;
    [SerializeField] private float carDestroyTimeBonus = 10f;
    [SerializeField] private int conePrice;
    [SerializeField] private int barrelPrice;
    [SerializeField] private int barrelScore;
    [SerializeField] private UI_handler uiText;
    [SerializeField] private Market_csript Market;
    [SerializeField] private AudioClip backGroundMusic;
    [SerializeField] private AudioSource audioController;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.carDestroyBonuseTime = carDestroyTimeBonus;
        audioController.clip = backGroundMusic;
        ScoreManager.isPaused = false;
        ScoreManager.conePrice = conePrice;
        ScoreManager.CarDestroyTime = coneCarDestroyTimer;
        Market.SetBarrelPrice(barrelPrice);
        Market.SetBarrelScore(barrelScore);
        uiText.DoomsDayTimerSet(doomsDayTimerStarTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
