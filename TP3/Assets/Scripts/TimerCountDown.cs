using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    public float time = 0;

    public bool playerIsAlive = true;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);
        timer.text = $"{min:00}:{sec:00}";
    }

    public void Death()
    {
        if (PlayerPrefs.GetInt("HighTimer") < Mathf.FloorToInt(time))
        {
            PlayerPrefs.SetInt("HighTimer",Mathf.FloorToInt(time));
        }
        PlayerPrefs.SetInt("ActualTimer",Mathf.FloorToInt(time));
    }
}