using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xcy.Battle;
using Xcy.Common;
using Xcy.SceneLoadManager;

public class PlayerGame3Manager : MonoSingleton<PlayerGame3Manager>
{
    private Health _health;
    public Health Health => _health;
    private CameraTarget _cameraTarget;

    private GameObject _wanJie;
    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
    }

    private void Start()
    {
        _wanJie=GameObject.Find("通关撒花");
        _wanJie.SetActive(false);
        _cameraTarget = GameObject.Find("CameraTarget")
            .GetComponent<CameraTarget>();
        _health = GetComponent<Health>();
        _health.onDied += OnDie3;
    }
    private void OnDie3()
    {
        StartCoroutine(OnDieCorout());
    }
    
    IEnumerator OnDieCorout()
    {
        Debug.Log("Die");
        _cameraTarget.isMoving = false;
        CheckPointSave.Instance.lastCheckPoint = 
            new Vector3(Camera.main.transform.position.x,Camera.main
            .transform.position.y,transform.position.z) -
            new Vector3(20, 0, 0);
        yield return new WaitForSeconds(1.0f);
        _cameraTarget.isMoving = true;
    }

    public void Finish()
    {
        _wanJie.SetActive(true);
        StartCoroutine(FinishCor());
    }

    IEnumerator FinishCor()
    {
        yield return new WaitForSeconds(5.0f);
//        SceneLoadManager.Instance.LoadNewScene("第三关过场");
        SceneManager.LoadScene("第三关过场");
    }
}
