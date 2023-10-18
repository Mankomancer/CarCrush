using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarellScript : MonoBehaviour
{
    public GameObject barellPrefab;
    public float timeLeft = 5;
    public float spawnTimer =10;
    public int barrelScore = 50;
    public int barrelCost = 20;
    public GameObject shopObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <0){
            timeLeft = spawnTimer;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-49,49), 1.6f, Random.Range(-49,49));    //1.6f approximate height to spawn object
            Instantiate (barellPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag=="Shop"){ //in case if barrel spawns in shop, delete it and make new one
            ScoreManager.AddScore(barrelScore);
            ScoreManager.AddMoney(barrelCost);
            timeLeft = -1;
            Destroy(this.gameObject);
        }
    }

    

}
