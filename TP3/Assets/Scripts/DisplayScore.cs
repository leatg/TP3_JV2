using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI actualTime;
    [SerializeField] private TextMeshProUGUI actualScore;
    void Awake()
    {
        //best time
        int hTime = PlayerPrefs.GetInt("HighTimer");
        float min = Mathf.FloorToInt(hTime / 60);
        float sec = Mathf.FloorToInt(hTime % 60);
        bestTime.text = $"{min:00}:{sec:00}";
        //best score
        int hScore = PlayerPrefs.GetInt("HighScore");
        bestScore.text = $"{hScore}";
        //actual time
        int aTime = PlayerPrefs.GetInt("ActualTimer");
        min = Mathf.FloorToInt(aTime / 60);
        sec = Mathf.FloorToInt(aTime % 60);
        actualTime.text = $"{min:00}:{sec:00}";
        //actual time
        int aScore = PlayerPrefs.GetInt("ActualScore");
        actualScore.text = $"{aScore}";
    }
}
