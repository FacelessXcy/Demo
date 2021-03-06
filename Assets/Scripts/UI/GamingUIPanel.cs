﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using Xcy.UIFramework;

public class GamingUIPanel : BasePanel
{
    public GameObject bloodCell;
    private Image fade;
    private Animation colorAnimation;
    public AnimationClip alpha;
    private Text _haiZaoNum;
    private Text _dingXiangYuNum;
    [SerializeField]private Image[] bloodCells=new Image[6];
    private bool _inThisPanel = false;

    public override void Start()
    {
        base.Start();
        fade = transform.Find("Fade").GetComponent<Image>();
        colorAnimation = fade.gameObject.GetComponent<Animation>();
        _haiZaoNum = transform.Find("HaiZao/HaiZaoNum").GetComponent<Text>();
        _dingXiangYuNum = transform.Find("DingXiangYu/DingXiangYuNum").GetComponent<Text>();
        if (bloodCell==null)
        {
            bloodCell = transform.Find("BloodBar/BloodCell").gameObject;
            for (int i = 0; i < 6; i++)
            {
                bloodCells[i] = bloodCell.transform.Find(i.ToString()).GetComponent<Image>();
            }
        }
    }

    private void Update()
    {
        if (_inThisPanel&&Input.GetKey(KeyCode.Escape))
        {
            UIManager.Instance.PushPanel(UIType.PauseMenu);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _inThisPanel = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PanelShow(canvasGroup);
        Time.timeScale = 1;
    }
    public override void OnPaused()
    {
        _inThisPanel = false;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0;
    }
    
    
    public override void OnResume()
    {
        _inThisPanel = true;
        PanelShow(canvasGroup);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    

    public void FadeAnimation()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        colorAnimation.clip = alpha;
        colorAnimation.Play();
        yield return 1f;
    }
    private void UpdateHpUI(float currentHp, float maxHp)
    {
        for (int i = 0; i < maxHp; i++)
        {
            if (i<currentHp)
            {
                bloodCells[i].enabled = true;
            }
            else
            {
                bloodCells[i].enabled = false;
            }
        }
    }
    //收集物UI
    private void UpdateHaiZaoCollection(int num)
    {
        if (_haiZaoNum==null)
        {
            _haiZaoNum = transform.Find("HaiZao/HaiZaoNum").GetComponent<Text>();
        }

        
        _haiZaoNum.text = num.ToString();
    }
    private void UpdateDingXiangYuCollection(int num)
    {
        if (_dingXiangYuNum==null)
        {
            _dingXiangYuNum = transform.Find("DingXiangYu/DingXiangYuNum").GetComponent<Text>();
        }
        _dingXiangYuNum.text = num.ToString();
    }

    public void UpdateAllGamingUI()
    {
        UpdateHpUI(PlayerManager.Instance.Health.currentHealth,
            PlayerManager.Instance.Health.maxHealth);
        UpdateHaiZaoCollection(InventorySystem.Instance
            .GetItemAmount(ItemType.HaiZao));
        UpdateDingXiangYuCollection(InventorySystem.Instance
            .GetItemAmount(ItemType.DingXiangYu));
    }
}
