using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            PlayerManager.Instance.Health.Kill();
        }
    }
}
