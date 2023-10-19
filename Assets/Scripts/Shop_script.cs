using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_script : MonoBehaviour
{
    /*
     ScoreManager - ir publiska statiska klase kas ir pieejama no jebkura skripta un ta nav pievienota nevienam objektam
    ScoreManager.AddMoney(cipari);  pievienot + naudiņas
    ScoreManager.GetMoney(); atgriež naudiņu vērtību cik tas ir
    ScoreManager.DecimateMoney(cipari);   nominuso izteretas naudiņas
    ScoreManager.ResetMoney(); nomet pa nullēm 
     */
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

    private void OnTriggerEnter(Collider other){//this will randomly give player score, if barell will spawn in market :/ need to thing about fix
        if (other?.tag=="Shop"){
            ScoreManager.AddScore(barrelScore);
            ScoreManager.AddMoney(barrelCost);
            GameObject.FindWithTag("OilSpawner").GetComponent<BarellScript>().timeLeft=-1;
            GameObject.FindWithTag("OilSpawner").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
    
}
