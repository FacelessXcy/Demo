﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;


namespace Xcy.SceneLoadManager
{
    using UnityEngine.SceneManagement;
    public class SceneLoadManager : MonoSingleton<SceneLoadManager>
    {
        private string _targetSceneName;
        private Sprite _backGroundImage;
        private string _tipText;
        public string TargetSceneName => _targetSceneName;
        public Sprite BackGroundImage => _backGroundImage;
        public string TipText => _tipText;

        public void LoadNewScene(string sceneName, Sprite
            backGroundImage = null, string tipText = null)
        {
            _targetSceneName = sceneName;
            if (backGroundImage != null)
            {
                _backGroundImage = backGroundImage;
            }

            if (tipText != null)
            {
                _tipText = tipText;
            }
            SceneManager.LoadScene("LoadingScene");
        }
    }
}