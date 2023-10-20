using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_csript : MonoBehaviour
{
    public int barrelScore = 50;
    public int barrelCost = 20;
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
}
