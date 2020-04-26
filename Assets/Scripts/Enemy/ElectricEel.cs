using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

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
        _currentDir = Vector2.Lerp(transform.right, _targetDir, 0.015f);
        transform.right = _currentDir;
        _rigidbody2D.MovePosition(transform.position+
                                  transform.right*moveSpeed*
                                  Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Test!");
            Destroy(this.gameObject);
        }
    }
}
