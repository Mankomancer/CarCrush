using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the namespace for TextMesh Pro

public class UI_handler : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text doomsDayClock;
    public TMP_Text howLongYouSurvived;
    private Vector2 aliveTime;
    private Vector2 doomsDayTime;
    private float doomClockStartTimer = 180;
    
    // The minimum and maximum scale of the text
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    // The speed of the scale and color change
    public float speed = 1f;

    // The colors to switch between
    public Color color1 = Color.white;
    public Color color2 = Color.red;
    
    void Start()
    {
        ScoreManager.doomsDayTimer = doomClockStartTimer;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.howLongYouSurvived += Time.deltaTime;
        if (ScoreManager.doomsDayTimer>1)
        {
            ScoreManager.doomsDayTimer-=Time.deltaTime;
        }
        aliveTime = FloatToTime(ScoreManager.howLongYouSurvived);
        doomsDayTime = FloatToTime(ScoreManager.doomsDayTimer);
        TextUpdater();
        TextEffect();
    }

    private void TextUpdater()
    {
        moneyText.text = ScoreManager.money.ToString();
        howLongYouSurvived.text = aliveTime.x.ToString("00") + ":" + aliveTime.y.ToString("00");
        doomsDayClock.text = doomsDayTime.x.ToString("00") + ":" + doomsDayTime.y.ToString("00");
        
    }
    private Vector2 FloatToTime(float rawTime)
    {
        Vector2 returnTime = new Vector2(Mathf.FloorToInt(rawTime / 60),Mathf.FloorToInt(rawTime % 60));
        return returnTime;

    }
    private void TextEffect()
    {
        // Calculate a value between 0 and 1 that oscillates over time
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // Lerp the scale and color based on the value
        doomsDayClock.transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
        doomsDayClock.color = Color.Lerp(color1, color2, t);
    }

    public void DoomsDayTimerSet(float setTimer)
    {
        doomClockStartTimer = setTimer;
    }
}
