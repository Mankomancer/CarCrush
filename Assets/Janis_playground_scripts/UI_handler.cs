using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the namespace for TextMesh Pro

public class UI_handler : MonoBehaviour
{
    public TMP_Text ScoreText; // Declare a public variable for the text component
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ScoreManager.GetScore().ToString();
    }
}
