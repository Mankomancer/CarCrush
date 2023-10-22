using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_csript : MonoBehaviour
{
    private int barrelScore = 25;
    private int barrelCost = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other?.tag=="Oil"){
            ScoreManager.AddScore(barrelScore);
            ScoreManager.AddMoney(barrelCost);
            GameObject.FindWithTag("OilSpawner").GetComponent<BarellScript>().timeLeft=-1;
            GameObject.FindWithTag("OilSpawner").GetComponent<AudioSource>().Play();
            ScoreManager.allOilObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    public void SetBarrelScore(int setScore)
    {
        barrelScore = setScore;
    }

    public void SetBarrelPrice(int setPrice)
    {
        barrelCost = setPrice;
    }
    
}
