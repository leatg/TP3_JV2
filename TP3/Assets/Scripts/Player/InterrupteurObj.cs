using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterrupteurObj : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private Vector3[] _positionArray = 
    {
        //near spawn platform
        new(30, 1, -25),
        new(30, 1, -10),
        new(30, 1, 0),
        new(30, 1, 10),
        new(30, 1, 25),
        //near spawn platform but with stairs access
        new(15, 4, 0),
        new(15, 4, -20),
        new(15, 4, 20),
        //cylinders
        new(-5.29f, 4, -19),
        new(-5.29f, 4, 19),
        //middle jump
        // new (-7.47f, 4, 0),
        //middle
        new(-7.47f, 1, 0),
        //back
        new(-25, 1, 0),
        //near back platform but with stairs access
        new(-25, 4, 0),
        //back corners
        new(-25, 1, -25),
        new(-25, 1, 25),
        //behind cylinders
        new(5.29f, 1, -25),
        new(5.29f, 1, 25),
    };
    
    public void Interact()
    {
        //add point
        scoreManager.GetScore = 1;
        //change position
        int newPosIndex;
        do
        {
            newPosIndex = Random.Range(0, _positionArray.Length);
        } while (_positionArray[newPosIndex] == gameObject.transform.position);
        gameObject.transform.position = _positionArray[newPosIndex];
    }
}