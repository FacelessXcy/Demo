using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiGuai : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Attackable _attackable;
    private Animator _animator;
    void Start()
    {
        _attackable = GetComponent<Attackable>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _attackable.onHide += OnHide;
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
