using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Battle;

public class DamagableWall : MonoBehaviour
{
    
    private Animation _animation;
    public AnimationClip animationClip;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animation = GetComponent<Animation>();
        _audioSource = GetComponent<AudioSource>();
        _health.onDamaged = GetDamage;
        _health.onDied = OnDie;
    }

    public void GetDamage(float damage,GameObject sourceGo)
    {
        StartCoroutine(GetDam());
    }
    IEnumerator GetDam()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }

    private void OnDie()
    {
        //Debug.Log("死亡");
        _audioSource.Play();
        _animation.clip = animationClip;
        _animation.Play();
        StartCoroutine(DestoryWall());
    }
    IEnumerator DestoryWall()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
