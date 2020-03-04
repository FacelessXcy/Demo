using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Xcy.Battle;

public class Enemy : MonoBehaviour
{
    private Transform ps;
    private ParticleSystem.EmissionModule emissionModule;
    
    public int currentHp = 3;
    public int maxHp = 3;
    public float damage = 1;
    private SpriteRenderer enemyRenderer;
    private Animator animator;
    private int attackID = Animator.StringToHash("attack");
    private int deadID = Animator.StringToHash("dead");
    private int moveID = Animator.StringToHash("attack");
    private Transform meleeTransform;
    ContactFilter2D contactFilter;
    public LayerMask layer;
    private AIHorizontalVersion ai;
    public bool inAttack;
    private Rigidbody2D r2d;
    public float power;//受伤害后所受的反推力
    //可在动画中修改

    public bool enemyGetDamage = false;
    public bool enemyDie = false;
    private Health _health;
    private void Awake()
    {
        ps = transform.Find("BurstParticle");
        enemyRenderer = GetComponent<SpriteRenderer>();
        meleeTransform = transform.Find("MeleeAttackPos");
        contactFilter.layerMask = layer;
        contactFilter.useLayerMask = true;
        animator = GetComponent<Animator>();
        ai = GetComponent<AIHorizontalVersion>();
        r2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _health = GetComponent<Health>();
        _health.onDamaged = GetDamage;
        _health.onDied = MonsterDie;
    }

    // Update is called once per frame
    void Update()
    {
        //currentAttackTime += Time.deltaTime;
        //if (currentAttackTime>=finishAttackTime)
        //{
        //    currentAttackTime = 0;
        //    inAttack = false;
        //}
        if (ai.attacking&&!inAttack&&(enemyGetDamage==false))
        {
            inAttack = true;
            Attack();         
        }
    }
    private void Attack()
    {
        animator.SetTrigger(attackID);
        StartCoroutine(takeMeleeDamage());
        //inAttack = true;
    }

    public void GetDamage(float damage,GameObject sourceGo)
    {
        enemyGetDamage = true;
        //伤害动画
        //声音
        StartCoroutine(ChangeColor());
        if (this.currentHp<=0)
        {
            MonsterDie();
            return;
        }

    }
    private void MonsterDie()
    {
        enemyDie = true;
        emissionModule = ps.GetComponent<ParticleSystem>().emission;
        emissionModule.rateOverTime = 120;
        animator.SetTrigger(deadID);
        StartCoroutine(DieIEnum());
    }

    IEnumerator DieIEnum()
    {
        yield return new WaitForSeconds(0.7f);
        emissionModule.rateOverTime = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }

    IEnumerator ChangeColor()
    {
        enemyRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        enemyRenderer.color = Color.white;
        enemyGetDamage = false;
    }


    IEnumerator takeMeleeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] hitCount = Physics2D.OverlapCircleAll(meleeTransform.position, 3.2f, layer);
        if (hitCount.Length != 0)
        {
            //Debug.Log("攻击到东西了");
            foreach (Collider2D item in hitCount)
            {
                if (item.gameObject != gameObject)
                {
                    Damageable damageable= item
                    .GetComponent<Damageable>();
                    if (damageable != null&&!enemyDie)
                    {
                        //Debug.Log("击中敌人");
                        damageable.GetDamage(damage,this.gameObject);
                    }
                }
            }
        }
    }

}
