using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarellScript : MonoBehaviour
{
    [SerializeField] private GameObject parentObject; // lai organizetu smuki visus barel objektus
    public GameObject barellPrefab;
    public float timeLeft = 5;
    public float spawnTimer =10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <0)
        {
            
            timeLeft = spawnTimer;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-49,49), 1.6f, Random.Range(-49,49));    //1.6f approximate height to spawn object
            GameObject spawn ;
            spawn =  Instantiate (barellPrefab, randomSpawnPosition, Quaternion.identity,parentObject.transform);
            ScoreManager.allOilObjects.Add(spawn);
            SpawnerTimeUpdater();            
        }
    }

     public void SpawnerTimeUpdater()
    {
        if (ScoreManager.GetScore()>=100 && ScoreManager.GetScore()<200){
            spawnTimer = 8;
        }
        else if (ScoreManager.GetScore()>=200 && ScoreManager.GetScore()<300){
            spawnTimer = 7;
        }
        else if (ScoreManager.GetScore()>=300 && ScoreManager.GetScore()<500){
            spawnTimer = 6;
        }
        else if (ScoreManager.GetScore()>=500 && ScoreManager.GetScore()<700){
            spawnTimer = 5;
        }
        else if (ScoreManager.GetScore()>=700 && ScoreManager.GetScore()<1000){
            spawnTimer = 4;
        }
        else if (ScoreManager.GetScore()>=1000){
            spawnTimer = 3;
        }
    }
}
