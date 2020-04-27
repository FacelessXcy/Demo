using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    
    private float _horizontalMove;
    private float _verticalMove;
    public float walkSpeed;
    public bool isMoving=true;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = false;
    }

    
    void Update()
    {
        _horizontalMove = PlayerInput.Instance.keyboardHorizontal;
        _verticalMove = PlayerInput.Instance.keyboardVertical;
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(transform.position +
                                  new Vector3(
                                      _horizontalMove * 
                                      walkSpeed*Time.fixedDeltaTime,
                                      _verticalMove * 
                                      walkSpeed* Time.fixedDeltaTime, 
                                      0));
    }
}
