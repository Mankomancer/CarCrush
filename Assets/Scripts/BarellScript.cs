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

    private int a = 100;
    private int b = 200;
    private int c = 300;
    private int d = 500;
    private int e = 700;
    private int f = 1000;
    
    private float a_time = 8f;
    private float b_time = 7f;
    private float c_time = 6f;
    private float d_time = 5f;
    private float e_time = 4f;
    private float f_time = 3f;
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
        if (ScoreManager.score>=a && ScoreManager.score<b){
            spawnTimer = a_time;
        }
        else if (ScoreManager.score>=b && ScoreManager.score<c){
            spawnTimer = b_time;
        }
        else if (ScoreManager.score>=c && ScoreManager.score<d){
            spawnTimer = c_time;
        }
        else if (ScoreManager.score>=d && ScoreManager.score<e){
            spawnTimer = d_time;
        }
        else if (ScoreManager.score>=e && ScoreManager.score<f){
            spawnTimer = e_time;
        }
        else if (ScoreManager.score>=f){
            spawnTimer = f_time;
        }
    }

     public void ScoreTimeSetter(Vector2 va, Vector2 vb, Vector2 vc, Vector2 vd, Vector2 ve, Vector2 vf)
     {
         a = Mathf.RoundToInt(va.x);
         b = Mathf.RoundToInt(vb.x);
         c = Mathf.RoundToInt(vc.x);
         d = Mathf.RoundToInt(vd.x);
         e = Mathf.RoundToInt(ve.x);
         f = Mathf.RoundToInt(vf.x);

         a_time = va.y;
         b_time = vb.y;
         c_time = vc.y;
         d_time = vd.y;
         e_time = ve.y;
         f_time = vf.y;

     }
}
