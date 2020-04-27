using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Xcy.Battle;
using Xcy.SceneLoadManager;

public class SecondBoss : MonoBehaviour
{
    public GameObject ballPrefab;
    public float attackRange;
    public float ballForce;
    private float _distance;
    private Health _health;
    private SpriteRenderer _spriteRenderer;
    private Transform _firePoint;
    private Transform _player;
    [FormerlySerializedAs("fireStepTime")] public float fireIntervalTime;
    private float _currentTime=0.0f;
    void Start()
    {
        _firePoint = transform.Find("FirePoint");
        _player = GameObject.FindWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
        _health.onDied += OnDied;
        _health.onDamaged += OnDamage;
    }

    private void Update()
    {
        _distance = (transform.position - _player.position).magnitude;
        //Debug.Log(_distance);
        if (_distance<=attackRange)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime>=fireIntervalTime)
            {
                _currentTime = 0;
                Fire();
            }
        }
        
    }

    private void Fire()
    {
        GameObject ball = GameObject.Instantiate(ballPrefab, 
            _firePoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce((_player
        .position-_firePoint.position).normalized*ballForce);
    }

    private void OnDied()
    {
        StartCoroutine(OnDiedIEnu());
        
    }

    IEnumerator OnDiedIEnu()
    {
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        SceneLoadManager.Instance.LoadNewScene("Game3Scene");
    }

    private void OnDamage(float damage,GameObject sourceGo)
    {
        StartCoroutine(ChangeColor());//受伤动画
    }
    IEnumerator ChangeColor()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.color = Color.white;
    }
}
