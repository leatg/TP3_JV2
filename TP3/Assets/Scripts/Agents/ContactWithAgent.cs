using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

public class ContactWithAgent : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    private ScoreManager _scoreManager;

    private TimerCountDown _timeManager;
    // Start is called before the first frame update
    void Awake()
    {
        _scoreManager = gameManager.GetComponent<ScoreManager>();
        _timeManager = gameManager.GetComponent<TimerCountDown>();
    }

    private void OnCollisionEnter(Collision thePlayer)
    {
        if (!thePlayer.gameObject.CompareTag("Player")) return;
        
        thePlayer.gameObject.GetComponent<MouseLook>()._isCursorLocked = false;
        
        _scoreManager.Death();
        _timeManager.Death();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
