using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEel : MonoBehaviour
{
    public float moveSpeed;
    
    private Transform _player;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _targetDir;
    private Vector2 _currentDir;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        _targetDir = _player.position - transform.position;
        _currentDir = Vector2.Lerp(transform.up, _targetDir, 0.015f);
        transform.up = _currentDir;
        _rigidbody2D.MovePosition(transform.position+
                                  transform.up*moveSpeed*
                                  Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Test!");
    }
}
