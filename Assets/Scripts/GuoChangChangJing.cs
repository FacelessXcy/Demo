using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.SceneLoadManager;

public class GuoChangChangJing : MonoBehaviour
{
    private TextMesh _textMesh;
    private GameObject _saHua;
    private void Start()
    {
        _textMesh = transform.Find("Text").GetComponent<TextMesh>();
        _textMesh.text = "";
        _saHua = transform.Find("通关撒花").gameObject;
        _saHua.SetActive(false);
        StartCoroutine(BackToStartScene());
    }

    IEnumerator BackToStartScene()
    {
        yield return new WaitForSeconds(3.0f);
        _textMesh.text = "游戏结束";
        _saHua.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneLoadManager.Instance.LoadNewScene("StartScene");
    }
}
