using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerMovement : MonoSingleton<PlayerMovement>
{

    private Rigidbody2D _rigidbody2D;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private Collider2D _collider2D;

    public float walkSpeed;
    public int maxJumpCount;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    [Header("=====角色状态=====")]
    public bool isGround;
    private bool _lastIsGround;
    public bool isJump;
    public int jumpCount;
    public bool shouldJump;

    private bool _fallToGround;
    private float _lastLeaveGroundMaxY=0.0f;
    private float _horizontalMove;
    private Vector3 _currentForward;

    private bool _isInOil=false;

    public bool IsInOil
    {
        get => _isInOil;
        set => _isInOil = value;
    }

    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        jumpCount = maxJumpCount;
    }


    private void Update()
    {
        if (PlayerInput.Instance.jump&&jumpCount>=0)
        {
            shouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        GroundMovementHandle();
        JumpHandle();
        _lastIsGround = isGround;
        //_lastLeaveGroundMaxY = transform.position.y;
    }


    private void GroundMovementHandle()
    {
        _horizontalMove = PlayerInput.Instance.keyboardHorizontal;

//        if (_isInOil)
//        {
            
//        }
//        else
//        {
//            _rigidbody2D.velocity=new Vector2(_horizontalMove*walkSpeed,
//                0);
//        }
        if (_isInOil)
        {
            _rigidbody2D.velocity=new Vector2(_horizontalMove*walkSpeed*0.25f,
                _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity=new Vector2(_horizontalMove*walkSpeed,
                _rigidbody2D.velocity.y);
        }

        if (_horizontalMove!=0)
        {
            transform.localScale=new Vector3(_horizontalMove>0?1:-1,1,1);
            PlayerAnimatorController.Instance.SetMoveBool(true);
        }
        else
        {
            PlayerAnimatorController.Instance.SetMoveBool(false);
        }
        _currentForward=new Vector3(transform.localScale.x,0,0);
    }

    private void JumpHandle()
    {
        if (isGround)
        {
            if (_lastLeaveGroundMaxY-transform.position.y>=0)
            {
                _fallToGround = true;
            }
            if (_lastIsGround!=isGround&&_fallToGround)//落地
            {
                jumpCount = maxJumpCount;
                isJump = false;
                shouldJump = false;
            }
            _lastLeaveGroundMaxY = transform.position.y;
        }
        else
        {
            if (_lastLeaveGroundMaxY<=transform.position.y)
            {
                _lastLeaveGroundMaxY = transform.position.y;
            }
        }
        if (_isInOil)
        {
            jumpCount = 2;
        }
        if (shouldJump&&isGround)
        {
            isJump = true;
            _rigidbody2D.velocity=new Vector2(_rigidbody2D.velocity.x,
                jumpForce);
            jumpCount--;
            shouldJump = false;
        }else if (shouldJump&&jumpCount>0&&!isGround)
        {
            _rigidbody2D.velocity=new Vector2(_rigidbody2D.velocity.x,
                jumpForce);
            jumpCount--;
            shouldJump = false;
        }
    }

    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f,
            groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.black;
        Gizmos.DrawSphere(groundCheck.position,0.1f);
    }
}
