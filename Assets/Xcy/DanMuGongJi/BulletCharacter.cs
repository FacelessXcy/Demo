using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcy.DanMu
{
    public class BulletCharacter : MonoBehaviour
    {
        
        [HideInInspector]public Vector3 dir;
        [HideInInspector]public float speed;
        [HideInInspector]public bool isMove;
        private FireMode _fireMode;
        private float _liveTime;
        private float _currentLiveTime;
        public FireMode FireMode
        {
            get => _fireMode;
            set => _fireMode = value;
        }
        private void OnEnable()
        {
            _liveTime = 5.0f;
            isMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMove)
            {
                BulletMove();
                _currentLiveTime += Time.deltaTime;
                if (_currentLiveTime>=_liveTime)
                {
                    if (_fireMode!=null)
                    {
                        _currentLiveTime = 0;
                        _fireMode.RecycleBullet(this);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

        public void BulletMove()
        {
            transform.position += dir * speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_fireMode!=null)
            {
                _fireMode.RecycleBullet(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}