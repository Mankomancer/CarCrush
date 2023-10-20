using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_script : MonoBehaviour
{
    [SerializeField] private GameObject loseUi;
    // Start is called before the first frame update
    void Start()
    {
        loseUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DoomTimerChecker();
    }

    private void DoomTimerChecker()
    {
        if (ScoreManager.doomsDayTimer<1)
        {
            ScoreManager.isPaused = true;
            Time.timeScale = 0f;
            loseUi.SetActive(true);
        }
    }
}
