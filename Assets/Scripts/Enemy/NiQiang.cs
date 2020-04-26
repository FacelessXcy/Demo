using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiQiang : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider2D;
    private float _waitTime=3.0f;
    private float _colliderTime = 1.3f;
    private float _currentTime = 0;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _animator.SetTrigger("attack");
    }

    
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime>=_colliderTime)
        {
            _collider2D.enabled = false;
        }
        if (_currentTime>=_waitTime)
        {
            _currentTime = 0;
            _collider2D.enabled = true;
            _animator.enabled = true;
            _animator.SetTrigger("attack");
        }
    }
}
