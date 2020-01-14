using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.UIFramework;

public enum UIType
{
    GamingUI,
    PauseMenu,
    StartMenuUI
}

public class UIManager : UIManagerBase
{
    
    public override void Create()
    {
        
    }

    public void FadeAnimation()
    {
        (GetPanel(UIType.GamingUI) as GamingUIPanel).FadeAnimation();
    }

    public void UpdateAllGamingUI()
    {
        (GetPanel(UIType.GamingUI) as GamingUIPanel).UpdateAllGamingUI();
    }

}
