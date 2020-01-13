using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{
    private CharacterController cc;
    private bool isJump;
    private bool isMove;
    public float moveSpeed = 4;             //移动的速度
    public float jumpSpeed = 4;             //跳跃的速度
    public float gravity = 1;               //重力

    private Vector3 moveDirection;

    private float h = 0;
    //private float v = 0;
    private Vector3 targetDir;
    private CollisionFlags flags;
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.1f )
        {
            targetDir = new Vector3(h, 0, 0);
            transform.LookAt(targetDir + transform.position);
            isMove = true;
        }

        if (Input.GetButton("Jump") && !isJump)
        {
            isJump = true;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y = jumpSpeed;
        }

        if (isJump)
        {
            //模拟物理,开始下降
            moveDirection.y -= gravity * Time.deltaTime;
            flags = cc.Move(moveDirection * Time.deltaTime);

            //人物碰撞到下面了
            if (flags == CollisionFlags.Below)
            {
                isJump = false;
            }
        }

        if (isMove)
        {
            cc.Move(transform.forward * moveSpeed * Time.deltaTime);
            isMove = false;
        }

    }
}
