//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PlayerController : MonoBehaviour
//{
//    public static PlayerController instance;
//
//    private new AudioSource audio;
//
//    public float maxVY;
//    public float minVY;
//    public float moveXDownSpeed;
//    public float moveXDownSpeedSky;
//    public float move = 0;
//    public float vertical;
//    private float horizontal;
//    public float walkSpeed = 5f;
//    public float flySpeed;
//    public float jumpSpeed = 1f;
//
//    //private Transform rightBullet;
//    //private Transform leftBullet;
//    public bool StartFaceLeft;
//    public Rigidbody2D r2d;
//    private Transform isGroundPoint;
//
//    public Animator m_animator;
//
//    private CircleCollider2D cc2d;
//    ContactFilter2D contactFilter;
//    public LayerMask layer;
//    private float currentJumpTime;
//    public float MaxJumpTime = 0.5f;
//    public float JumpPower;
//
//
//    private int moveID = Animator.StringToHash("move");
//    private int jumpID = Animator.StringToHash("jump");
//
//    public bool isJumping = false;
//
//    public bool isPaused = false;
//
//    private void Awake()
//    {
//        instance = this;
//        audio = GetComponent<AudioSource>();
//        //rightBullet = transform.Find("RightBulletPos");
//        //leftBullet = transform.Find("LeftBulletPos");
//        r2d = transform.GetComponent<Rigidbody2D>();
//        isGroundPoint = transform.Find("isGround").transform;
//        m_animator = GetComponent<Animator>();
//        cc2d = transform.GetComponent<CircleCollider2D>();
//        contactFilter.layerMask = layer;
//        contactFilter.useLayerMask = true;
//        if (StartFaceLeft)
//        {
//            Vector3 temp = transform.localScale;
//            temp.x = -1;
//            transform.localScale = temp;
//        }
//        else
//        {
//            Vector3 temp = transform.localScale;
//            temp.x = 1;
//            transform.localScale = temp;
//        }
//    }
//
//    private void Update()
//    {
//        EscInput();
//    }
//    void FixedUpdate()
//    {
//        if (PlayerAttribute.instance!=null)
//        {
//            if (!PlayerAttribute.instance.isDead)
//            {
//                QuickSave();
//                PlayerMove();
//            }
//            else
//            {
//                 r2d.velocity = Vector2.up * 15;
//            }
//
//        }
//    }
//
//    private void PlayerMove()
//    {
//        //Debug.Log(Physics2D.gravity.y);
//        horizontal = Input.GetAxis("Horizontal");
//        if (isGrounded())
//        {
//            vertical = 0;
//        }
//        else
//        {
//            vertical = r2d.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime;
//        }
//        if (PlayerAttack.instance.inAttack && isGrounded())
//        {
//            horizontal = 0;
//        }
//        //Debug.Log(vertical);
//        if (horizontal != 0)
//        {
//            move = horizontal * walkSpeed;
//            m_animator.SetBool(moveID, move != 0);
//        }
//        else
//        {
//            if (isGrounded())
//            {
//                if (move < 0)
//                {
//                    move = Mathf.Clamp(r2d.velocity.x + moveXDownSpeed * Time.fixedDeltaTime, -walkSpeed, 0);
//                    m_animator.SetBool(moveID, move != 0);
//                }
//                else if (move > 0)
//                {
//                    move = Mathf.Clamp(r2d.velocity.x - moveXDownSpeed * Time.fixedDeltaTime, 0, walkSpeed);
//                    m_animator.SetBool(moveID, move != 0);
//                }
//            }
//            else
//            {
//                if (move < 0)
//                {
//                    move = Mathf.Clamp(r2d.velocity.x + moveXDownSpeedSky * Time.fixedDeltaTime, -flySpeed, 0);
//                    m_animator.SetBool(moveID, move != 0);
//                }
//                else if (move > 0)
//                {
//                    move = Mathf.Clamp(r2d.velocity.x - moveXDownSpeedSky * Time.fixedDeltaTime, 0, flySpeed);
//                    m_animator.SetBool(moveID, move != 0);
//                }
//            }
//
//        }
//
//        vertical = Mathf.Clamp(vertical, minVY, maxVY);
//        r2d.velocity = new Vector2(move, vertical);
//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
//        {
//            isJumping = true;
//            m_animator.SetTrigger(jumpID);
//            r2d.velocity = new Vector2(move, jumpSpeed * MaxJumpTime * JumpPower);
//
//        }
//        if (move != 0)
//        {
//            Vector3 temp = transform.localScale;
//            temp.x = (move > 0) ? 1 : -1;
//            transform.localScale = temp;
//        }
//    }
//
//    public bool isGrounded()
//    {
//        Collider2D[] resultCollider = new Collider2D[10];
//        int hitCount = Physics2D.OverlapCircle(isGroundPoint.position, 0.5f, contactFilter, resultCollider);
//        if (hitCount != 0)
//        {
//            return true;
//        }
//        return false;
//
//    }
//    private void QuickSave()
//    {
//        if (Input.GetKeyDown(KeyCode.F2))
//        {
//            //Debug.Log(CheckPoint.instance==null);
//
//            CheckPoint.instance.UpdateCheckPoint(transform.position);
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.layer == LayerMask.NameToLayer("Collection"))
//        {
//            audio.clip = Resources.Load<AudioClip>("collectionOn");
//            audio.Play();
//        }
//    }
//
//    private void EscInput()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;
//            if (isPaused)
//            {
//                Paused();
//            }
//            else
//            {
//                UnPaused();
//            }
//        }
//    }
//    public void Paused()
//    {
//        Time.timeScale = 0;
//        UpdateUI.instance.PanelShow(UpdateUI.instance.PausedMenu);
//    }
//    public void UnPaused()
//    {
//        Time.timeScale = 1;
//        UpdateUI.instance.PanelHide(UpdateUI.instance.PausedMenu);
//    }
//
//}
