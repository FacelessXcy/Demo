using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;
using Xcy.DanMu;

public class ThirdBoss : MonoBehaviour
{
    public float attackRange;
    private float _distance;
    private Health _health;
    private SpriteRenderer _spriteRenderer;
    private Transform _player;
    public float fireIntervalTime;
    private float _currentTime=0.0f;
    private FireMode _fireMode;
    void Start()
    {
        _fireMode = GetComponent<FireMode>();
        _player = GameObject.FindWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
        _health.onDied += OnDied;
        _health.onDamaged += OnDamage;
    }
    void Update()
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
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1:
                _fireMode.FireShotGun();
                break;
            case 2:
                _fireMode.FireRound();
                break;
            case 3:
                _fireMode.FirRoundGroup();
                break;
            case 4:
                _fireMode.FireTurbine();
                break;
        }
    }

    private void OnDied()
    {
        PlayerGame3Manager.Instance.Finish();
        gameObject.SetActive(false);
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
