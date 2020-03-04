using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xcy.Common;

public class SaveManager : MonoSingleton<SaveManager>
{
    
    public string sceneName;
    public int sceneIndex;
    public  float maxHp =6;
    public float currentHp =6;
    public float damage=1;
    private Save _saveData;

    public override void Awake()
    {
        _saveData=new Save();
        base.Awake();
    }

    //按照当前存储的状态创建存档文件
    public Save CreateSaveGO()
    {
        SaveData();
        return _saveData;
    }
    
    public void LoadData()//加载场景后，载入数据
    {
        PlayerManager.Instance.Health.maxHealth = _saveData.maxHp;
        PlayerManager.Instance.Health.currentHealth =
            _saveData.currentHp;
        PlayerAttack.Instance.damage = _saveData.damage;
        UIManager.Instance.UpdateAllGamingUI();
    }

    private void SaveData()//切换场景时，保存数据
    {
        _saveData.maxHp = PlayerManager.Instance.Health.maxHealth;
        _saveData.currentHp = PlayerManager.Instance.Health.currentHealth;
        _saveData.damage = PlayerAttack.Instance.damage;
        _saveData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //sceneName = SceneManager.GetActiveScene().name;
    }

    public void SetSaveData(Save data)
    {
        _saveData = data;
    }

    public Save GetSaveData()
    {
        return _saveData;
    }

}
