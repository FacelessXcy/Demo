using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;
using Xcy.Common;
using Xcy.SceneLoadManager;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public ParticleSystem moveParticleSystem;
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
        _health = GetComponent<Health>();
        _health.onDamaged += OnDamage;
        _health.onHealed += OnHeal;
        _health.onDied += OnDie;
        if (GameManager.Instance.needLoadData)
        {
            GameManager.Instance.needLoadData = false;
            GameManager.Instance.LoadData();
        }
        moveParticleSystem = transform.Find("MoveParticle").GetComponent<ParticleSystem>();
        _psEmissionModule = moveParticleSystem.emission;
        _spriteRenderer=GetComponent<SpriteRenderer>();
    }


    private void OnDamage(float realDamageAmount,GameObject damageSource)
    {
        isDamaging = true;//
        ShakeCamera.instance.enabled = true;//镜头抖动
        UIManager.Instance.UpdateAllGamingUI();//更新血量UI
        StartCoroutine(ChangeColor());//受伤动画
        StartCoroutine(ParticlesSystemChange());
    }

    private void OnDie()
    {
        StartCoroutine(OnDieCorout(CheckPointSave.Instance.GetLastCheckPoint()));
    }
    
    private void OnHeal(float healAmount)
    {
        
    }
    
    
    IEnumerator OnDieCorout(Vector3 checkPoint)
    {
        PlayerAnimatorController.Instance.SetDeadTrigger();
        UIManager.Instance.FadeAnimation();
        yield return new WaitForSeconds(0.8f);
        ResetHP();
        ResetPosition(checkPoint);
        UIManager.Instance.UpdateAllGamingUI();
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
