using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_tester_DoNotUse : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Awake()
    {
        // This prevents the object from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        score = ScoreManager.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            score = ScoreManager.GetScore();
            Debug.Log(score);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {    
            ScoreManager.AddScore(10);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ScoreManager.ResetScore();
        }
    }
}
