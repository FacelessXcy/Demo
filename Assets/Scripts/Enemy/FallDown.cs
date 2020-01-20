
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    
    public float horizontalDistance;
    public float verticalDistance;

    private Transform _player;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
    }

    
    void Update()
    {
        if (Mathf.Abs(_player.position.x - transform.position.x) <=
            horizontalDistance)
        {
            if (Mathf.Abs(_player.position.y-transform.position.y)<=verticalDistance
                &&(_player.position.y - transform.position.y)<=0)
            {
                if (_rigidbody2D.isKinematic)
                {
                    _rigidbody2D.isKinematic = false;
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.up*verticalDistance);
        Gizmos.DrawRay(transform.position, Vector3
        .right*horizontalDistance);
        
    }
}
