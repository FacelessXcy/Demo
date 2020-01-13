using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToGround : MonoBehaviour
{


    private AudioSource audioSource;
        private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
//        if ((collision.gameObject.layer==LayerMask.NameToLayer("Ground")&&
//             PlayerController.instance.r2d.velocity.y!=0)
//            ||collision.gameObject.layer==LayerMask.NameToLayer("JumpPoint"))
//        {
//            audioSource.Play();
//            PlayerController.instance.isJumping = false;
//            //Debug.Log("跌落");
//
//        }
    }
}
