using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Score_tester_DoNotUse : MonoBehaviour
{
    [SerializeField]private GameObject pauseScreen;
    private Input_controlls controlls;
    private int score;
    // Start is called before the first frame update
    void Awake()
    {
        // This prevents the object from being destroyed when loading a new scene
      //  DontDestroyOnLoad(gameObject);
        controlls = new Input_controlls();
        controlls.Gameplay.Pause.performed += ctx => pause();
        pauseScreen.SetActive(false);
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

        if (Time.timeScale == 0f&&pauseScreen.activeSelf==false)
        {
            Time.timeScale = 1f;
        }

       
    }

  void pause()
    {
        if (pauseScreen.activeSelf==false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (pauseScreen.activeSelf==true)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

  private void OnEnable()
  {
      controlls.Gameplay.Enable();
  }

  private void OnDisable()
  {
      controlls.Gameplay.Disable();
  }
}
