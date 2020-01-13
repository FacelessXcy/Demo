using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PickUpItem : MonoBehaviour
{
    public AudioClip pickupClip;
    public UnityAction<InventorySystem> onPick;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _rotate;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        ActivePickUp();
    }

    private void ActivePickUp()
    {
        //_canPickup = true;
        _rigidbody.isKinematic = true;
        _rotate = true;
        gameObject.layer = LayerMask.NameToLayer("PickUp");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        PlayerInput playerInput =
            other.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            Debug.Log("Player In");
            if (pickupClip != null)
            {
                playerInput.GetComponent<AudioSource>()
                    .PlayOneShot(pickupClip);
            }
            if (onPick != null)
            {
                onPick.Invoke(InventorySystem.Instance);
                Destroy(gameObject);
            }
        }
    }
}
