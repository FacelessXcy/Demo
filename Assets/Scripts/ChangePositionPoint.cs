using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionPoint : MonoBehaviour
{
    public Vector3 nextPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(
                ChangePos(other.transform, nextPosition));
            
        }
    }

    IEnumerator ChangePos(Transform player,Vector3 pos)
    {
        PlayerAnimatorController.Instance.SetDeadTrigger();
        UIManager.Instance.FadeAnimation();
        yield return new WaitForSeconds(0.85f);
        player.position = pos;
    }

}
