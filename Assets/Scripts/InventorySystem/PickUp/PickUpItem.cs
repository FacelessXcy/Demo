using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PickUpItem : MonoBehaviour
{
    public bool UseGravityAtBegin;
    public float rotatingSpeed = 360f;
    public AudioClip pickupClip;
    public UnityAction<InventorySystem> onPick;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _rotate;
//    private bool _canPickup;
    //[HideInInspector] public PickUpSubManager pickUpSubManager;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
//        _canPickup = false;
        ActivePickUp();
    }

    private void Update()
    {
        if (_rotate)
        {
            transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime, Space.Self);
        }
    }

    IEnumerator StartRotate()
    {
        yield return new WaitForSeconds(2.0f);
        ActivePickUp();
    }

    private void ActivePickUp()
    {
        //_canPickup = true;
        transform.up = Vector3.up;
        transform.position += Vector3.up;
        _rigidbody.isKinematic = true;
        _rotate = true;
        gameObject.layer = LayerMask.NameToLayer("PickUp");
    }
    private void OnTriggerEnter(Collider other)
    {
//        if (!_canPickup)
//        {
//            return;
//        }
        PlayerInput playerInput =
            other.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            if (pickupClip != null)
            {
                playerInput.GetComponent<AudioSource>()
                    .PlayOneShot(pickupClip);
            }

            if (onPick != null)
            {
//                _canPickup = false;
                onPick.Invoke(InventorySystem.Instance);
                Destroy(gameObject);
            }
        }
    }
}
