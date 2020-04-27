using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.SceneLoadManager;

public class GuoChangChangJing : MonoBehaviour
{
    private TextMesh _textMesh;
    private void Start()
    {
        _textMesh = transform.Find("Text").GetComponent<TextMesh>();
        _textMesh.text = "";
        StartCoroutine(BackToStartScene());
    }

    IEnumerator BackToStartScene()
    {
        yield return new WaitForSeconds(3.0f);
        _textMesh.text = "游戏结束";
        yield return new WaitForSeconds(3.0f);
        SceneLoadManager.Instance.LoadNewScene("StartScene");
    }
}
