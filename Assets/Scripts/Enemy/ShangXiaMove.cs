using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShangXiaMove : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public float waitTime;
    private Transform _curDest;
    private float _currentTime = 0;
    private bool _isStop;
    private int _curIndex = 0;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _curDest = points[_curIndex%points.Length];
    }

    private void FixedUpdate()
    {
        if (_isStop)
        {
            _currentTime += Time.fixedDeltaTime;
            if (_currentTime>=waitTime)
            {
                _currentTime = 0;
                _curIndex++;
                _isStop = false;
                _curDest = points[_curIndex%points.Length];
            }
        }

        if (Mathf.Abs(_curDest.position.y-transform.position.y)<=0.1f)
        {
            _isStop = true;
        }
        else
        {
            float dir = (_curDest.position.y - transform.position.y) > 0
                ? 1.0f
                : -1.0f;
            _rigidbody2D.MovePosition(new Vector2(transform.position
            .x,transform.position.y+dir*Time.fixedDeltaTime*speed));
        }
    }
}
