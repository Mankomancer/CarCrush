using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Quit_button : MonoBehaviour
{
    public void loadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void quitGame()
    {
        Application.Quit();
    }
    public void NewGame(string scenename)
    {
        SceneManager.LoadScene(scenename);
       // ScoreManager.ResetScore();
    }
}
