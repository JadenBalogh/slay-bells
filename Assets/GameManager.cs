using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static bool IsGameOver { get => instance.isGameOver; set => instance.isGameOver = value; }
    private bool isGameOver = false;

    public static int Kills { get => instance.kills; set => instance.kills = value; }
    private int kills = 0;

    [Header("UI")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        winScreen.SetActive(IsGameOver);
        killsText.text = "Naughty List: " + Kills + " / 100";
    }
}
