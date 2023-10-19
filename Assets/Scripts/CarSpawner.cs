using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject autoPrefab;
    public float timeLeft = 5;
    public float spawnTimer =5;
    public GameObject autoInGame;

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        MinimumAmountOfCars(); //in case players try to rush at the start of the game
        
        timeLeft -= Time.deltaTime; //this will spawn cars even in marketplace, maybe need to find solution
        if (timeLeft <0){
            timeLeft = spawnTimer;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-49,49), 1f, Random.Range(-49,49));    //1.6f approximate height to spawn object
            Instantiate (autoPrefab, randomSpawnPosition, Quaternion.identity);
            SpawnerTimeUpdater();            
        }
    }

    public void MinimumAmountOfCars(){
        if (GameObject.FindGameObjectsWithTag("Auto").Length<=4){
            timeLeft=-1;
        }
    }

    public void SpawnerTimeUpdater(){
        if (ScoreManager.GetScore()>=100){
            spawnTimer = 6;
        }
        else if (ScoreManager.GetScore()>=200){
            spawnTimer = 7;
        }
        else if (ScoreManager.GetScore()>=300){
            spawnTimer = 8;
        }
    }
}
