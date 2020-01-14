using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;

public class PlayerAnimatorController : MonoSingleton<PlayerAnimatorController>
{
    private Animator _animator;

    private int _attackID = Animator.StringToHash("attack");
    private int _jumpID = Animator.StringToHash("jump");
    private int _moveID = Animator.StringToHash("move");
    private int _deadID = Animator.StringToHash("dead");
    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetJumpTrigger()
    {
        _animator.SetTrigger(_jumpID);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(_attackID);
    }

    public void SetMoveBool(bool value)
    {
        _animator.SetBool(_moveID,value);
    }

    public void SetDeadTrigger()
    {
        _animator.SetTrigger(_deadID);
    }

}
