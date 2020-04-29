using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xcy.SceneLoadManager;
using Xcy.UIFramework;

public class StartMenuPanel : BasePanel
{
    public Sprite backGround;
    private Button _startGameBtn;
    private Button _exitGameBtn;
    
    public override void Start()
    {
        base.Start();
        TempAudioManager.Instance.ChangeMusic(0);
        _startGameBtn =
            transform.Find("StartGame").GetComponent<Button>();
        _exitGameBtn=transform.Find("ExitGame").GetComponent<Button>();
        _startGameBtn.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadNewScene("Game1Scene",
            false,false,backGround,null);
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
