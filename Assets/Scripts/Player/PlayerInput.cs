﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Xcy.Common;
using Xcy.Input;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerInput : MonoSingleton<PlayerInput>
{
    [HideInInspector]public bool gameIsPaused;
    [Header("===== Key Settings=====")]
    public string keyAttack;
    
    
    private MyButton _buttonJump=new MyButton();
    private MyButton _buttonAttack=new MyButton();
    private MyButton _buttonWalkRight=new MyButton();
    private MyButton _buttonWalkLeft=new MyButton();

    [Header("===== Output Signals=====")]
    public float keyboardHorizontal;
    public float keyboardVertical;
    public bool jump;
    public bool attack;
    
    public override void Awake()
    {
        _destoryOnLoad = true;
        base.Awake();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        keyboardHorizontal = 0;
        keyboardVertical = 0;
    }


    private void Update()
    {
        if (gameIsPaused)
        {
            ResetSingal();
            return;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        _buttonAttack.Tick(Input.GetKeyDown(keyAttack));
        _buttonJump.Tick(Input.GetButtonDown("Jump"));


        keyboardHorizontal = Input.GetAxis("Horizontal");
        keyboardVertical = Input.GetAxis("Vertical");
        jump = _buttonJump.onPressed;
        attack = _buttonAttack.onPressed;
    }


    private void ResetSingal()
    {
        jump = false;
        attack = false;
        keyboardHorizontal = 0;
        keyboardVertical = 0;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
    }

}
