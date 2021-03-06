﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Battle
{
    public class Damageable : MonoBehaviour
    {
        private Health _health;
        private void Start()
        {
            _health = GetComponent<Health>();
        }


        public void GetDamage(float damage,GameObject damageResource)
        {
            //Debug.Log(name+"被"+damageResource.name+"击中",gameObject);
            if (_health!=null)
            {
                _health.TakeDamage(damage,damageResource);
            }
        }
    }
}