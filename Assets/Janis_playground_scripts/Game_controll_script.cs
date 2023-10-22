using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_controll_script : MonoBehaviour
{
    [Header("Loose Settings")]
    [SerializeField] private float doomsDayTimerStarTime;
    [SerializeField] private int maxCarCount;
    [Header("Game Settings")]
    [SerializeField] private float coneCarDestroyTimer = 5f;
    [SerializeField] private float carDestroyTimeBonus = 10f;
    [SerializeField] private int conePrice;
    [SerializeField] private int barrelPrice;
    [SerializeField] private int barrelScore;
    [Header("Script objects")]
    [SerializeField] private UI_handler uiText;
    [SerializeField] private Market_csript Market;
    [SerializeField] private AudioClip backGroundMusic;
    [SerializeField] private AudioSource audioController;

    [Header("Barrel spawn score + time")] 
    [SerializeField] private BarellScript barel;
    [SerializeField] private Vector2 onesSpawnTime;
    [SerializeField] private Vector2 twoSpawnTime;
    [SerializeField] private Vector2 threeSpawnTime;
    [SerializeField] private Vector2 fourSpawnTime;
    [SerializeField] private Vector2 fiveSpawnTime;
    [SerializeField] private Vector2 sixSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        barel.ScoreTimeSetter(onesSpawnTime,twoSpawnTime,threeSpawnTime,fourSpawnTime,fiveSpawnTime,sixSpawnTime);
        ScoreManager.maxCarCount = maxCarCount;
        ScoreManager.carDestroyBonuseTime = carDestroyTimeBonus;
        audioController.clip = backGroundMusic;
        audioController.enabled = false;
        audioController.enabled = true;
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
