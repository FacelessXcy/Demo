using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBubble : MonoBehaviour
{

    private Collider2D _collider2D;
    
    
    private void Start()
    {
        
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (rigidbody2D!=null)
        {
            rigidbody2D.velocity=new Vector2(rigidbody2D.velocity.x, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.IsInOil = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.IsInOil = false;
        }
    }
}
