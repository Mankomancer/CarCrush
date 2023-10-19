using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarellScript : MonoBehaviour
{
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
        if (timeLeft <0){
            timeLeft = spawnTimer;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-49,49), 1.6f, Random.Range(-49,49));    //1.6f approximate height to spawn object
            Instantiate (barellPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

}
