using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xcy.SceneLoadManager;
using Xcy.UIFramework;

public class PauseMenuPanel : BasePanel
{
    private Button _saveBtn;
    private Button _loadBtn;
    private Button _backToStartBtn;
    private Button _resumeGameBtn;
    public override void Start()
    {
        base.Start();
        _saveBtn = transform.Find("SaveGame").GetComponent<Button>();
        _loadBtn=transform.Find("LoadGame").GetComponent<Button>();
        _backToStartBtn=transform.Find("BackToStart").GetComponent<Button>();
        _resumeGameBtn=transform.Find("ResumeGame").GetComponent<Button>();
        _saveBtn.onClick.AddListener(GameManager.Instance.Save);
        _loadBtn.onClick.AddListener(GameManager.Instance.Load);
        _backToStartBtn.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance
                .LoadNewScene("StartScene");
        });
        _resumeGameBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.PopPanel();
        });
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PanelShow(canvasGroup);
        GameManager.Instance.PauseGame();
    }

    public override void OnExit()
    {
        PanelHide(canvasGroup);
        GameManager.Instance.ResumeGame();
    }
}
