using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    private float _liveTime=5.0f;
    private float _currentLiveTime;

    private void Update()
    {
        _currentLiveTime += Time.deltaTime;
        if (_currentLiveTime>=_liveTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer)==
            "CameraCollider")
        {
            Destroy(this.gameObject);
        }
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable!=null)
        {
            damageable.GetDamage(damage,this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
