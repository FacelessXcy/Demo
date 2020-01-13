using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEditor;

public class AIHorizontalVersion : MonoBehaviour
{
    #region 变量

    [Tooltip("敌人跳跃能力")]
    public float jumpPower=3100;
    [Tooltip("碰撞监测点大小")]
    public float a;
    [Tooltip("移动速度")]
    public int moveSpeed;
    [Tooltip("追击速度")]
    public int chaseSpeed;
    private int horizontal;
    private Rigidbody2D r2d;
    [Tooltip("到达目的地后，停止时间")]
    public float stayTime;
    [Tooltip("单次移动时间")]
    public float maxMoveTime = 3;
    [Tooltip("墙壁所在的layer")]
    public string groundLayer;
    [Tooltip("玩家所在的Layer")]
    public string playerLayer;
    [Tooltip("玩家的Transform")]
    public Transform playerTransform;
    [Tooltip ("前方视野范围")]
    public Vector2 size;

    [Tooltip("后方视野范围判定器的半径")]
    public float radius;
    [Tooltip("攻击范围")]
    public float attackRadius;
    public Transform upCheckPoint;
    public Transform downCheckPoint;
    public Transform backViewPoint;
    public Transform forwardViewPoint;
    public Transform attackRange;
    private Animator animator;

    [Header("以下为private变量")]
    [SerializeField] private float nowMoveTime = 0;
    [SerializeField] private float currentStayTime;

    [SerializeField] private int random2 = 0;
    private Enemy enemy;
    private bool upObs;
    private bool downObs;

    private int temp2 = 0;
    private bool hasTurned = false;
    private float turnTime=0.25f;
    private float currentTurnTime=0;

    private bool canJump=true;
    public bool attacking = false;
    private bool forwardFind=false;
    private bool backFind=false;

    private int moveID = Animator.StringToHash("move");

    #endregion
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        r2d = GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
        r2d.gravityScale =20;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        ViewCircle();
        ViewBox();
        AttackRange();
        currentStayTime += Time.fixedDeltaTime;
        if (!enemy.enemyGetDamage&&!enemy.enemyDie)
        {

            if ((forwardFind||backFind) == false)
            {
                Move();
                if (currentStayTime >= stayTime + maxMoveTime)
                {
                    currentStayTime = 0;
                }
            }
            if (forwardFind||backFind)
            {
                 if (attacking)
                 {
                    r2d.velocity = Vector2.zero;
                    animator.SetBool(moveID,false);
                    //Debug.Log("攻击");
                 }
                 else
                 {
                        MoveToPlayer(playerTransform.position);
                 }
            }
        }
        else
        {
            currentStayTime = 0;
            //int face = (transform.localScale.x > 0) ? 1 : -1;
            //r2d.velocity = new Vector2(500*Time.fixedDeltaTime*(-face), r2d.velocity.y);
        }

    }

    private void MoveToPlayer(Vector3 playerPos)
    {
        //stillMove = true;
        Vector3 tempM = playerPos - transform.position;
  
        horizontal = chaseSpeed * (tempM.x >= 0 ? 1 : -1);
        if (horizontal!=0)
        {
            Vector3 tempI = transform.localScale;
            tempI.x = (horizontal > 0) ? 1 : -1;
            transform.localScale = tempI;
        }
        animator.SetBool(moveID,true);
        r2d.velocity = new Vector2(horizontal * Time.deltaTime, 0);
    }

    private void Move()
    {
        
        if (currentStayTime >= maxMoveTime && currentStayTime <= maxMoveTime + stayTime)
        {
            r2d.velocity = new Vector2(0,r2d.velocity.y);
            animator.SetBool(moveID,false);
            return;
        }
        animator.SetBool(moveID,true);
        nowMoveTime += Time.fixedDeltaTime;
        if (nowMoveTime >= maxMoveTime)
        {
            nowMoveTime = 0;
            random2 = Random.Range(0, 100);
            if (random2 <= 33) temp2 = -1;
            else if (random2 <= 66) temp2 = 0;
            else if (random2 <= 100) temp2 = 1;
        }
        if (horizontal!=0&&!hasTurned)
        {
            Vector3 temp = transform.localScale;
            temp.x = (horizontal > 0) ? 1 : -1;
            transform.localScale = temp;
        }
        DownMeetObs();
        UpMeetObs();
        UpdateTime();
 
        if (!upObs&&downObs)
        {
            if (canJump)
            {
                r2d.velocity = new Vector2(horizontal*Time.fixedDeltaTime,jumpPower*Time.fixedDeltaTime);
                canJump = false;
                StartCoroutine("changeCanJump");
            }
        }
        else if (upObs&&downObs)
        {
                if (temp2 == -1) temp2 = 1;
                else temp2 = -1;
                
                Vector3 temp = transform.localScale;
                temp.x = -1;
                transform.localScale = temp;

                hasTurned = true;

        }
        horizontal = moveSpeed * temp2;
        r2d.velocity = new Vector2(horizontal * Time.fixedDeltaTime, r2d.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);
        
    }


    private void DownMeetObs()//前方下侧障碍物判断
    {
        Collider2D[] hit=  Physics2D.OverlapCircleAll(new Vector2(downCheckPoint.position.x,downCheckPoint.position.y),a );
        foreach (Collider2D item in hit)
        {
            if (item.gameObject.layer==LayerMask.NameToLayer(groundLayer)|| item.gameObject.layer ==LayerMask.NameToLayer("DamageableWall"))
            {
                downObs = true;
                return;
            }
        }
        downObs = false;
        return;
    }
    private void UpMeetObs()//前方上侧障碍物判断
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2(upCheckPoint.position.x, upCheckPoint.position.y), a);
        foreach (Collider2D item in hit)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer(groundLayer) || item.gameObject.layer == LayerMask.NameToLayer("DamageableWall"))
            {
                upObs= true;
                return;
            }
        }
        upObs= false;
        return;
    }

    private void ViewCircle()//后方视野判定
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2(backViewPoint.position.x ,backViewPoint.position.y), radius);
        foreach (Collider2D item in hit)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer(playerLayer))
            {
                backFind = true;
                return;
            }
        }
        backFind = false;
        return;
    }
    private void ViewBox()//前方视野判定
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(new Vector2(forwardViewPoint.position.x, forwardViewPoint.position.y), 
            size, LayerMask.NameToLayer(playerLayer)); 
        foreach (Collider2D item in hit)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer(playerLayer))
            {
                forwardFind = true;
                //Debug.Log("前方有敌人");
                return;
            }
        }
        forwardFind = false;
        return;
    }

    private void AttackRange()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2(attackRange.position.x, attackRange.position.y), attackRadius);
        foreach (Collider2D item in hit)
        {
            if (item.gameObject.layer == LayerMask.NameToLayer(playerLayer))
            {
                attacking = true;
                return;
            }
        }
       attacking = false;
        return;
    }
    /***********************碰撞检测范围辅助观察函数*****************************/
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1.0f, 0, 0, 0.1f);
        Handles.DrawSolidDisc(downCheckPoint.position, Vector3.back, a);

        Handles.color = new Color(1.0f, 0, 0, 0.1f);
        Handles.DrawSolidDisc(upCheckPoint.position, Vector3.back, a);

        Handles.color = new Color(1.0f, 1.0f, 0, 0.1f);
        Handles.DrawSolidDisc(backViewPoint.position, Vector3.back, radius);

        //Physics2D.OverlapBoxAll(,);
        Handles.color = new Color(1.0f, 1.0f, 0.5f, 0.1f);
        Handles.DrawWireCube(forwardViewPoint.position, size);

        Handles.color = new Color(0f, 1.0f, 0, 0.1f);
        Handles.DrawSolidDisc(attackRange.position, Vector3.back, attackRadius);
    }

    private void OnDrawGizmosSelected()
    {
       
    }
    private void UpdateTime()
    {
        
        currentTurnTime += Time.fixedDeltaTime;
        if (currentTurnTime>=turnTime&&hasTurned==true)
        {
            currentTurnTime = 0;
            hasTurned = false;
        }
    
    }


    IEnumerator changeCanJump()
    {
        yield return new WaitForSeconds(1);
        canJump = true;
    }
}
