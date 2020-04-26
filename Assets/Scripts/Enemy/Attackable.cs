using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class Attackable : MonoBehaviour
{
    public float damage;
    public float intervalTime=1.5f;
    public bool hideSelf;
    private float _curTime;
    private bool _active;
    private void Start()
    {
        _curTime = intervalTime;
        _active = true;
    }

    private void Update()
    {
        if (!_active)
        {
            _curTime += Time.deltaTime;
            if (_curTime>=intervalTime)
            {
                _active = true;
                _curTime = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_active)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable!=null)
            {
                damageable.GetDamage(damage,this.gameObject);
            }
            _active = false;
            if (hideSelf)
            {
                gameObject.SetActive(false);
            }
            //Destroy(this.gameObject);
        }
    }
}
