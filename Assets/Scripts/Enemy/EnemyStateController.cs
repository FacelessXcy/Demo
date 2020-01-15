using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public enum EnemyState
{
    Idle,
    Walk,
    Dead,
    Attack
}
public class EnemyStateController : MonoBehaviour
{
    public EnemyState enemyState;

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();


        _health.onDied += OnDied;
        _health.onDamaged += OnDamaged;
    }


    private void OnDied()
    {
        
    }

    private void OnDamaged(float realDamage,GameObject sourceGo)
    {
        
    }

}
