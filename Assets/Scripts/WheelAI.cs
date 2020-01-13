using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAI : MonoBehaviour
{
    public Transform[] destination;
    private int index = 0;
    private Rigidbody2D r2d;
    public float moveSpeed;
    public float stayTime;
    [SerializeField] private float currentStayTime = 0;
    [SerializeField] private bool isStay = false;
    [SerializeField] private float distan;
    [Tooltip("防止抽搐")]
    [Range(0, 2)]
    public float distance;

    private Vector2 tempVector2;
    private Vector2 nextPos;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        nextPos = destination[0].position;

    }
    private void FixedUpdate()
    {
        if (isStay)
        {
            currentStayTime += Time.fixedDeltaTime;
            if (currentStayTime>=stayTime)
            {
                currentStayTime = 0;
                nextPos= ChangeDestination();
                isStay = false;
            }
                return;
        }
        Move(nextPos);



    }

    private Vector2 GetVector2()
    {
        return transform.position;
    }

    private Vector2 ChangeDestination()
    {
        index++;
        int temp = index % destination.Length;
        return destination[temp].position;
    }
    private Vector2 Direction(Vector2 pos)
    {
        Vector2 temp;
        temp = pos - new Vector2( transform.position.x,transform.position.y);

        return temp;
    }

    private void Move(Vector2 pos)
    {
        if (Vector2.Distance(transform.position,pos)<=0.5f)
        {
            isStay = true;
            r2d.velocity = Vector2.zero;
            return;
        }
        distan = Vector2.Distance(transform.position,pos);
        tempVector2 = Direction(pos).normalized;
        r2d.velocity = new Vector2(moveSpeed * tempVector2.x * Time.fixedDeltaTime, 
            moveSpeed * tempVector2.y * Time.fixedDeltaTime);

    }



}
