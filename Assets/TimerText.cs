using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    private float gameTimer = 0f;
    private TextMeshProUGUI textbox;

    private void Awake()
    {
        textbox = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (GameManager.IsGameOver)
        {
            return;
        }

        gameTimer += Time.deltaTime;
        textbox.text = "Time: " + gameTimer.ToString("0.0");
    }
}
