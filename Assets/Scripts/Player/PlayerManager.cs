﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;
using Xcy.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public float power;
    public ParticleSystem particleSystem;
    private Health _health;
    public Health Health => _health;
    
    private ParticleSystem.EmissionModule _psEmissionModule;
    private SpriteRenderer _spriteRenderer;

    public bool isDamaging=false;
    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
    }

    private void Start()
    {
        particleSystem = transform.Find("MoveParticle").GetComponent<ParticleSystem>();
        _psEmissionModule = particleSystem.emission;
        _spriteRenderer=GetComponent<SpriteRenderer>();
        
        _health = GetComponent<Health>();
        _health.onDamaged = OnDamage;
        _health.onHealed = OnHeal;
        _health.onDied = OnDie;
    }


    private void OnDamage(float realDamageAmount,GameObject damageSource)
    {
        isDamaging = true;//
        PlayerMovement.Instance.Rigidbody2D.AddForce(((Vector2)(transform
        .position -damageSource.transform.position)*power));
        ShakeCamera.instance.enabled = true;//镜头抖动
        UpdateUI.instance.UpdateHpUI((int)_health.currentHealth, 
            (int)_health.maxHealth);//更新血量UI
        StartCoroutine(ChangeColor());//受伤动画
        StartCoroutine(ParticlesSystemChange());
        //音效
//        if (PlayerAttribute.instance.currentHp <= 0&&!PlayerAttribute.instance.isDead)
//        {
//            PlayerAttribute.instance.OnDie(CheckPoint.instance.GetLastCheckPoint());
//        }
    }

    private void OnDie()
    {
        StartCoroutine(OnDieCorout(CheckPoint.Instance.GetLastCheckPoint()));
    }
    
    private void OnHeal(float healAmount)
    {
        
    }
    
    
    IEnumerator OnDieCorout(Vector3 checkPoint)
    {
        //PlayerController.instance.m_animator.SetTrigger("dead");//死亡动画
        UpdateUI.instance.FadeAnimation();//屏幕变黑
        yield return new WaitForSeconds(0.95f);
        ResetHP();
        ResetPosition(checkPoint);
        UpdateUI.instance.UpdateHpUI(_health.currentHealth,_health.maxHealth);
    }
    private void ResetHP()
    {
        _health.Respawn();
    }
    private void ResetPosition(Vector3 checkPoint)
    {
        transform.position = checkPoint;
    }
    
    IEnumerator ParticlesSystemChange()
    {
        _psEmissionModule.rateOverTime = 150;
        yield return new WaitForSeconds(0.1f);
        _psEmissionModule.rateOverTime = 10;


    }

    IEnumerator ChangeColor()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.color = Color.white;
        isDamaging = false;
    }
    
}
