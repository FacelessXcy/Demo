using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xcy.SceneLoadManager;
using Xcy.UIFramework;

public class StartMenuPanel : BasePanel
{

    private Button _startGameBtn;
    private Button _exitGameBtn;
    
    public override void Start()
    {
        base.Start();
        _startGameBtn =
            transform.Find("StartGame").GetComponent<Button>();
        _exitGameBtn=transform.Find("ExitGame").GetComponent<Button>();
        _startGameBtn.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadNewScene("Game1Scene");
        });
        _exitGameBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void LoadGame()
    {
        GameManager.Instance.LoadOnFile(true);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
}
