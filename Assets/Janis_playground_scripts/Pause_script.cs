using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Pause_script : MonoBehaviour
{
    [SerializeField]private GameObject pauseScreen;
    private Input_controlls controlls;
  
    // Start is called before the first frame update
    void Awake()
    {
        controlls = new Input_controlls();
        controlls.Gameplay.Pause.performed += ctx => pause();
        pauseScreen.SetActive(false);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //šis new game gadījumā noņem pauzi;
        if (Time.timeScale == 0f&&!ScoreManager.isPaused)
        {
            Time.timeScale = 1f;
        }

       
    }

  void pause()
    {
        if (!ScoreManager.isPaused)
        {
            ScoreManager.isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (ScoreManager.isPaused)
        {
            ScoreManager.isPaused = false;
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
