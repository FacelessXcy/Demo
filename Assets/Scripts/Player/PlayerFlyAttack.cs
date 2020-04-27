using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;

public class PlayerFlyAttack : MonoSingleton<PlayerFlyAttack>
{
    public float attackIntervalTime;
    public GameObject bulletPrefab;
    public float bulletForce;
    private float _currentTime;
    private bool _inCool=false;
    private Transform _firePoint;
    
    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
    }
    private void Start()
    {
        _firePoint = transform.Find("FirePoint");
    }

    void Update()
    {
        if (_inCool)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime>=attackIntervalTime)
            {
                _inCool = false;
                _currentTime = 0;
            }
        }
        Attack();
    }

    private void Attack()
    {
        if (PlayerInput.Instance.attack&&!_inCool)
        {
            _inCool = true;
            GameObject go = GameObject.Instantiate(bulletPrefab,
                _firePoint.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(transform
            .right*bulletForce);
        }
    }
    
    
    
    
}
