using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;

    public void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("Points",score);
    }

    public int getScore
    {
        get => score;
        set
        {
            score += value;
            PlayerPrefs.SetInt("Points",score);
            scoreText.text = $"Score : {score}";
        }
    }

    public void Death()
    {
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
        PlayerPrefs.SetInt("ActualScore",score);
    }
}
