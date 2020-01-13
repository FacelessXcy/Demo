using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class Trap : MonoBehaviour
{
    private int damage=1;
    public string layer;
  

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(layer);
        if (collision.gameObject.layer==LayerMask.NameToLayer(layer))
        {
            //Debug.Log(LayerMask.NameToLayer(layer));
            if (!PlayerManager.Instance.isDamaging)
            {
                PlayerManager.Instance.isDamaging = true;
                StartCoroutine(GiveDamage(collision, damage));
            }
        }
        
    }

    IEnumerator GiveDamage(Collider2D collision,int damage)
    {
        yield return 2;
        PlayerManager.Instance.GetComponent<Damageable>().GetDamage
        (damage,gameObject);

    }
}
