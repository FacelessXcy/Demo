using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;
using Xcy.Common;
using Xcy.SceneLoadManager;

public class GameManager : MonoSingleton<GameManager>
{
    public bool needLoadData = false;
    public override void Awake()
    {
        base.Awake();
        //Debug.Log(audioSource);
    }
    
    public void LoadData()
    {
        SaveManager.Instance.LoadData();
    }

    //切换场景时保存进度，不允许手动保存
    public void SaveOnFile()
    {
        Save save = SaveManager.Instance.CreateSaveGO();
        ES3.Save<Save>("SaveGame",save);
        Debug.Log("SaveGame");
    }
    public void LoadOnFile(bool loadNewScene=true)
    {
        Save save = ES3.Load<Save>("SaveGame");
        Debug.Log("LoadGame");
        SaveManager.Instance.SetSaveData(save);
        if (loadNewScene)
        {
            SceneLoadManager.Instance.LoadNewScene(save.sceneIndex+1,
            false,true);
        }
    }
    
    public void PauseGame()
    {
        PlayerInput.Instance.PauseGame();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PlayerInput.Instance.ResumeGame();
        Time.timeScale = 1;
    }

    public void CreateGameManager()
    {
    }
    
    //private void SaveByJson()
    //{
    //    Save save = CreateSaveGO();
    //    string filePath = Application.dataPath + "/SaveData" + "/SaveByJson.txt";
    //    //Debug.Log("filePath 完成");
    //    string jsonStr = JsonMapper.ToJson(save);
    //    //Debug.Log("jsonStr 完成");
    //    StreamWriter sw = new StreamWriter(filePath);
    //    sw.Write(jsonStr);
    //    sw.Close();
    //    if (File.Exists(filePath))
    //    {
    //        //Debug.Log("保存成功");
    //    }
    //}
    //private void LoadByJson()
    //{
    //    string filePath = Application.dataPath + "/SaveData" + "/SaveByJson.txt";
    //    if (File.Exists(filePath))
    //    {
    //        //Debug.Log("读取成功");
    //        StreamReader sr = new StreamReader(filePath);
    //        string jsonStr = sr.ReadToEnd();
    //        sr.Close();
    //        Save save = JsonMapper.ToObject<Save>(jsonStr);
    //        setGame(save);

    //    }

    //}
}
