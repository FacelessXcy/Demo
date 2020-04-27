using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class PingYiGuai : MonoBehaviour
{    
    
    private Health _health;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
        _health.onDied += OnDied;
        _health.onDamaged += OnDamage;
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
    }

    private void OnDamage(float damage,GameObject sourceGo)
    {
        StartCoroutine(ChangeColor());//受伤动画
    }
    IEnumerator ChangeColor()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.color = Color.white;
    }

}
