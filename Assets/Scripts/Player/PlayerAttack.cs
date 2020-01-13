using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;
using Xcy.Common;
using static UnityEngine.ParticleSystem;

public class PlayerAttack : MonoSingleton<PlayerAttack>
{

    public float damage;
    public LayerMask layerAttack;

    private AudioSource _audioSource;
    private UpdateUI _UI;
    private Transform _meleeTransform;
    ContactFilter2D _contactFilter;
    private Animator _animator;
    private int attackID = Animator.StringToHash("attack");

    public bool inAttack;
    public float backPower;//攻击到目标后的后坐力
    //可在动画中修改


    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
        
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _UI = GameObject.Find("Canvas").GetComponent<UpdateUI>();
        _meleeTransform = transform.Find("MeleeAttackPos");
        _contactFilter.layerMask = layerAttack;
        _contactFilter.useLayerMask = true;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (PlayerInput.Instance.attack&&!inAttack)
        {         
            inAttack = true;
            _animator.SetTrigger(attackID);
            //声音
            _audioSource.clip = Resources.Load<AudioClip>("PlayerAttack");
            _audioSource.Play();
            StartCoroutine(takeMeleeDamage());
        }
    }



    IEnumerator takeMeleeDamage()
    {
        yield return new WaitForSeconds(0.3f);
        Collider2D[] hitDamageable = Physics2D.OverlapCircleAll(_meleeTransform.position, 3.2f, layerAttack);
        if (hitDamageable.Length != 0)
        {
            //Debug.Log("攻击到东西了");
            foreach (Collider2D item in hitDamageable)
            {
                if (item.gameObject != gameObject)
                {
                    Damageable damageable =
                        item.GetComponent<Damageable>();
                    if (damageable!=null)
                    {
                        damageable.GetDamage(damage,this.gameObject);
                    }
                }
            }
        }
    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color=Color.blue; 
//        Gizmos.DrawSphere(_meleeTransform.position,3.2f);
//    }
}
