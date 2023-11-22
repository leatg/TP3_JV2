using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCantJumpDown : MonoBehaviour
{
    private Transform _player;
    private Vector3 _spawnPos;
    private float _elapseTime;

    private void Start()
    {
        _player = gameObject.transform;
        _spawnPos = _player.position;
    }

    void Update()
    {
        _elapseTime += Time.deltaTime;

        if (_elapseTime > 5 && _player.position.x < 34 && _player.position.z < 34)
        {
            _elapseTime = 0;
            _spawnPos = _player.position;
        }
        
        if (_player.position.y < -1.5f)
            _player.position = _spawnPos;
    }
}
