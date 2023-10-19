using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the namespace for TextMesh Pro

public class UI_handler : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text doomsDayClock;
    public TMP_Text howLongYouSurvived;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdater();
    }

    private void TextUpdater()
    {
        moneyText.text = ScoreManager.money.ToString();
        howLongYouSurvived.text = ScoreManager.howLongYouSurvived.ToString();
        doomsDayClock.text = ScoreManager.doomsDayTimer.ToString();
    }
}
