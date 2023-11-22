using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

public class ContactWithAgent : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    private ScoreManager scoreManager;

    private TimerCountDown timeManager;
    // Start is called before the first frame update
    void Awake()
    {
        scoreManager = gameManager.GetComponent<ScoreManager>();
        timeManager = gameManager.GetComponent<TimerCountDown>();
    }

    private void OnCollisionEnter(Collision thePlayer)
    {
        if (!thePlayer.gameObject.CompareTag("Player")) return;
        
        thePlayer.gameObject.GetComponent<MouseLook>()._isCursorLocked = false;
        
        scoreManager.Death();
        timeManager.Death();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
