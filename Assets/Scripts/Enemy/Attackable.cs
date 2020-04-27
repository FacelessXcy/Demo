using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Xcy.Battle;

public class Attackable : MonoBehaviour
{
    public float damage;
    public float intervalTime=1.5f;
    public bool hideSelfColPlayer;
    public bool hideSelfColWall;
    private float _curTime;
    private bool _active;
    public UnityAction onHide;
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
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable!=null)
        {
            if (_active)
            {
                _active = false;
                damageable.GetDamage(damage,this.gameObject);
                if (hideSelfColPlayer)
                {
                    if (onHide!=null)
                    { 
                        onHide();
                    }
                    else
                    { 
                        gameObject.SetActive(false);
                    }
                }
                //Destroy(this.gameObject);
            }
        }
        else if(hideSelfColWall)
        {
            gameObject.SetActive(false);
        }
        
    }
}
