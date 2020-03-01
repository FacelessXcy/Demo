using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class Trap : MonoBehaviour
{
    private int damage=1;
    public string layer;

    public virtual void Awake()
    {
        //Debug.Log("Trap");
    }

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

public class TrapSon : Trap
{
    public override void Awake()
    {
        base.Awake();
        //Debug.Log("TrapSon");
    }
}
public class TrapSonSon : TrapSon
{
    public LayerMask mask;
    public int layerInt;
    public override void Awake()
    {
//        base.Awake();
//        Debug.Log(mask+"     mask");
//        Debug.Log(mask.value+"  mask.value");
//        Debug.Log(LayerMask.NameToLayer("Trap")+"  LayerMask.NameToLayer(Trap)");
//        Debug.Log(LayerMask.LayerToName(13) +
//                  "  LayerMask.NameToLayer(13)");
//        layerInt = 1 << 13;
//        Debug.Log(layerInt+"   layerInt");
//        mask= 1 << 13;
//        Debug.Log(mask.value+"  mask.value");
        //Debug.Log(gameObject.layer);
    }
}