using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;


namespace Xcy.SceneLoadManager
{
    using UnityEngine.SceneManagement;

    public enum SceneLoadMode
    {
        LoadByIndex,
        LoadByName
    }

    public class SceneLoadManager : MonoSingleton<SceneLoadManager>
    {
        private SceneLoadMode _loadMode;
        private string _targetSceneName;
        private int _targetSceneIndex;
        private Sprite _backGroundImage;
        private string _tipText;
        public string TargetSceneName => _targetSceneName;
        public int TargetSceneIndex => _targetSceneIndex;
        public SceneLoadMode LoadMode => _loadMode;
        public Sprite BackGroundImage => _backGroundImage;
        public string TipText => _tipText;

        public void LoadNewScene(int sceneIndex,bool 
            saveData=false,bool loadData=false, Sprite backGroundImage =
             null,
            string tipText = null)
        {
            if (saveData)
            {
                GameManager.Instance.SaveOnFile();
            }

            GameManager.Instance.needLoadData = loadData;
            _loadMode = SceneLoadMode.LoadByIndex;
            if (sceneIndex==(SceneManager.sceneCountInBuildSettings-1))
            {
                Debug.LogWarning("要加载的场景为Loading界面");
            }
            _targetSceneIndex = sceneIndex;
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

        public void LoadNewScene(string sceneName,bool 
            saveData=false,bool loadData=false, Sprite backGroundImage = null,
            string tipText = null)
        {
            if (saveData)
            {
                GameManager.Instance.SaveOnFile();
            }
            GameManager.Instance.needLoadData = loadData;
            _loadMode = SceneLoadMode.LoadByName;
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