﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Xcy.Battle;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Idle,
    Walk,
    Dead,
    Attack,
    Seek
}
public class EnemyStateController : MonoBehaviour
{
    public float damage=2;
    public float walkSpeed;
    [HideInInspector]public EnemyState enemyState;
    private Health _health;
    private Vector2 _currentForward;
    public Vector2 CurrentForward => _currentForward;
    private float _horizontal;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _enemyRenderer;
    private Transform _eyesPosition;
    private Transform _meleeTransform;
    private Transform _playerTransform;
    public Transform EyesPosition => _eyesPosition;

    private float _exitSeekTimer;
    private float _walkTimer;
    private float _attackTimer;
    private float _idleTimer;

    private bool _needExitSeek = false;
    private int _moveID = Animator.StringToHash("move");
    private int _attackID=Animator.StringToHash("attack");
    private int _jumpID = Animator.StringToHash("jump");
    private int _deadID = Animator.StringToHash("dead");
    


    private bool _inAttack;
    
//    private void Awake()
//    {
//        
//    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _enemyRenderer = GetComponent<SpriteRenderer>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _health = GetComponent<Health>();
        _meleeTransform = transform.Find("MeleeAttackPos");
        _health.onDied += OnDied;
        _health.onDamaged += OnDamaged;
        _eyesPosition = transform.Find("EyesPosition");
    }


    private void Update()
    {
        //Debug.Log(enemyState);
        switch (enemyState)
        {
            case EnemyState.Dead:
                StopMove();
                return;
            case EnemyState.Attack:
                StopMove();
                if (UpdateTime(ref _attackTimer,1f,Time.deltaTime))
                {
                    _inAttack = false;
                    //enemyState = EnemyState.Idle;
                }
                if (!_inAttack)
                {
                    Attack();
                }
                break;
            case EnemyState.Seek:
                if (_needExitSeek)
                {
                    //Debug.Log(_exitSeekTimer);
                    if (UpdateTime(ref _exitSeekTimer,2f,Time.deltaTime))
                    {
                        _needExitSeek = false;
                        enemyState = EnemyState.Idle;
                        break;
                    }
                }
                MoveToPlayer();
                break;
            case EnemyState.Walk:
                if (UpdateTime(ref _walkTimer,2.5f,Time.deltaTime))
                {
                    enemyState = EnemyState.Idle;
                    break;
                }
                Walk();
                break;
            case EnemyState.Idle:
                StopMove();
                if (UpdateTime(ref _idleTimer,2.5f,Time.deltaTime))
                {
                    StartWalk();
                }
                break;
        }
        
        _currentForward =
            new Vector2(transform.localScale.x > 0 ? 1:-1, 0);
        Debug.DrawRay(_eyesPosition.position,_currentForward*40,Color.green);
    }

    public void ExitSeek()
    {
        _needExitSeek = true;
    }

    public void StartWalk()
    {
        enemyState = EnemyState.Walk;
        _horizontal = walkSpeed*((Random.Range(0, 2) == 0) ? -1 : 1);
    }

    public void StartSeek()
    {
        enemyState = EnemyState.Seek;
        _animator.SetBool(_moveID,true);
    }

    private void StopMove()
    {
        _rigidbody2D.velocity=new Vector2(0,_rigidbody2D.velocity.y);
        _animator.SetBool(_moveID,false);
    }

    public void StartIdle()
    {
        enemyState = EnemyState.Idle;
    }

    public void StartAttack()
    {
        enemyState = EnemyState.Attack;
        _animator.SetBool(_moveID,false);
    }


    private void Attack()
    {
        _inAttack = true;
        StartCoroutine(TakeMeleeDamage());
        _animator.SetTrigger(_attackID);
    }
    IEnumerator TakeMeleeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] hitCount = Physics2D.OverlapCircleAll(_meleeTransform.position, 3.2f);
        if (hitCount.Length != 0)
        {
            //Debug.Log("攻击到东西了");
            foreach (Collider2D item in hitCount)
            {
                if (item.gameObject.CompareTag("Player"))
                {
                    Damageable damageable= item
                        .GetComponent<Damageable>();
                    if (damageable != null)
                    {
                        //Debug.Log("击中敌人");
                        damageable.GetDamage(damage,this.gameObject);
                    }
                }
            }
        }
    }
    private void Walk()
    {
        _animator.SetBool(_moveID,true);
        Vector3 tempI = transform.localScale;
        tempI.x = (_horizontal > 0) ? 1 : -1;
        transform.localScale = tempI;
        _animator.SetBool(_moveID,true);
        _rigidbody2D.velocity = new Vector2(
            _horizontal * Time.deltaTime, _rigidbody2D.velocity.y);
    }

    private void MoveToPlayer()
    {
        _animator.SetBool(_moveID,true);
        Vector3 tempM = _playerTransform.position - transform.position;
        _horizontal = walkSpeed * (tempM.x >= 0 ? 1 : -1);
        if (_horizontal!=0)
        {
            Vector3 tempI = transform.localScale;
            tempI.x = (_horizontal > 0) ? 1 : -1;
            transform.localScale = tempI;
        }
        _animator.SetBool(_moveID,true);
        _rigidbody2D.velocity = new Vector2(_horizontal * Time
        .deltaTime, _rigidbody2D.velocity.y);
    }
    
    private bool UpdateTime(ref float timer,float maxTime,float stepTime)
    {
        bool hasFinished = false;
        timer += stepTime;
        if (timer>=maxTime)
        {
            timer = 0;
            hasFinished = true;
        }
        return hasFinished;
    }
    
    
    private void OnDied()
    {
        StartCoroutine(Die());
        enemyState = EnemyState.Dead;
        _animator.SetTrigger(_deadID);
    }

    IEnumerator Die()
    {
        yield return new  WaitForSeconds(0.5f);
        Debug.Log("怪物阵亡");
        gameObject.SetActive(false);
    }

    private void OnDamaged(float realDamage,GameObject sourceGo)
    {
        Debug.Log("怪物被攻击到");
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {
        _enemyRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        _enemyRenderer.color = Color.white;
    }

}
