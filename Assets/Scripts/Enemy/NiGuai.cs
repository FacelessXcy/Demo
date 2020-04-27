using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class NiGuai : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Attackable _attackable;
    private Animator _animator;
    private Health _health;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer=GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
        _health.onDied += onDied;
        _health.onDamaged += OnDamage;
        _attackable = GetComponent<Attackable>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _attackable.onHide += OnHide;
    }
    
    private void OnDamage(float damage,GameObject go)
    {
        StartCoroutine(ChangeColor());//受伤动画
    }
    IEnumerator ChangeColor()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.color = Color.white;
    }
    
    private void onDied()
    {
        gameObject.SetActive(false);
    }

    private void OnHide()
    {
        
        StartCoroutine(OnHideIenu());
    }

    IEnumerator OnHideIenu()
    {
        _animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _rigidbody2D.isKinematic = true;
        }
    }
}
