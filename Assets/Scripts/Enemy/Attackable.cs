using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class Attackable : MonoBehaviour
{
    public float damage;
    private Collider2D _collider2D;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = GetComponent<Damageable>();
        if (damageable!=null)
        {
            damageable.GetDamage(damage,this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
