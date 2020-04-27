using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float launchForce;
    public float launchRange;
    public float liveTime;
    private float _distance;
    private float _currentTime=0.0f;
    private Transform _player;
    private Rigidbody2D _rigidbody2D;
    private bool _hasLaunch;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        if (!_hasLaunch)
        {
            _distance = (transform.position - _player.position).magnitude;
            //Debug.Log(_distance);
            if (_distance<=launchRange)
            {
                _hasLaunch = true;
                Vector3 dir = _player.position-transform.position;
                _rigidbody2D.AddForce(dir*launchForce);
                transform.right = dir;
            }
        }
        else
        {
            _currentTime += Time.deltaTime;
            if (_currentTime>=liveTime)
            {
                _currentTime = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
