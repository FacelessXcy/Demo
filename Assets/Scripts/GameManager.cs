using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
   
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                //寻找该类实例，
                instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
                //Debug.Log("Instance==Null");
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
            //Debug.Log("GameManagerNull");
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                DontDestroyOnLoad(go);
                instance = go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(gameObject);
            instance = GetComponent<GameManager>();
        }
        else if (instance != null)
        {
            Debug.Log("GameManagerNotNull");
            Destroy(gameObject);
        }


        if (GetComponent<CheckPoint>()==null)
        {
            gameObject.AddComponent<CheckPoint>();
        }
       
        //Debug.Log(audioSource);
    }

    //保存载入管理
    public Save CreateSaveGO()
    {

        GlobalObject.Instance.SaveData();
        Save save=new Save();
        save.sceneName = SceneManager.GetActiveScene().name;

        save.playerPos = GlobalObject.Instance.GetSavePlayerPos();

        //保存当前Hp
        save.currentHp = GlobalObject.Instance.currentHp;
        //保存当前钻石数量
        save.diamondCount = GlobalObject.Instance.GetIntValueInDictionary(GlobalObject.Instance.collectionDic, Collection.ItemType.diamond);

        GameObject diamondsRoot = GameObject.Find("DiamondCollections");
        //GameObject[] diamondsO = GameObject.FindGameObjectsWithTag("diamond");//获取所有的collection
        Transform[] diamondsO = diamondsRoot.GetComponentsInChildren<Transform>(true);

        foreach (Transform go in diamondsO)
        {
            //Debug.Log(go.gameObject.name);
            save.diamondsPosActive.Add(go.gameObject.activeInHierarchy);


        }
        //Debug.Log(Application.persistentDataPath);

        //localPos和worldPos可能会有问题
        //获取所有可破坏墙壁坐标
        GameObject damageableWallsRoot = GameObject.Find("DamageableWalls");
        Transform[] damageableWallsO = damageableWallsRoot.GetComponentsInChildren<Transform>(true); ;

        foreach (Transform item in damageableWallsO)
        {
            save.damageableWallsActive.Add(item.gameObject.activeInHierarchy);
        }
        //当前音乐名称
        if (AudioManager.Instance.AudioSource.clip!=null)
        {
            save.musicName = AudioManager.Instance.AudioSource.clip.name;

        }
        //Debug.Log("Create 完成");
        return save;
    }


    private void setGame(Save save)
    {
        //Debug.Log(save.playerPos);
        GlobalObject.Instance.SetSavePlayerPos(save.playerPos);
        //Debug.Log(GlobalObject.Instance.GetSavePlayerPos());
        GlobalObject.Instance.currentHp = save.currentHp;
        GlobalObject.Instance.SetIntValueInDictionary(GlobalObject.Instance.collectionDic, Collection.ItemType.diamond,save.diamondCount);
        //GlobalObject.Instance.monstersPos = save.monstersPos;
        GameObject diamondRoot = GameObject.Find("DiamondCollections");
        Transform[] diamondAc = diamondRoot.transform.GetComponentsInChildren<Transform>();
        //Debug.Log(save.diamondsPosActive.Count);
        for (int i = 0; i < save.diamondsPosActive.Count; i++)
        {
            GlobalObject.Instance.diamondsAct.Add(save.diamondsPosActive[i]);
             //GlobalObject.Instance.diamondsPos.Add( new Vector3((float)save.diamondsPosX[i], (float)save.diamondsPosY[i], (float)save.diamondsPosZ[i]));
        }

        //GlobalObject.Instance.damageableWallsPos = save.damageableWallsPos;
        for (int i = 0; i < save.damageableWallsActive.Count; i++)
        {
            GlobalObject.Instance.damageableWallsAct.Add(save.damageableWallsActive[i]);
            //GlobalObject.Instance.damageableWallsPos.Add( new Vector3((float)save.damageableWallsPosX[i], (float)save.damageableWallsPosY[i], (float)save.damageableWallsPosZ[i]));
        }
        AudioManager.Instance.AudioSource.clip =Resources.Load<AudioClip>( save.musicName);
        
        AudioManager.Instance.csol = AudioManager.ChangeSceneOrLoad.Load;
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


    public void Save()
    {
        //GlobalObject.Instance.SaveData();
        //SaveByJson();
        GlobalObject.Instance.SetSavePlayerPos(PlayerAttribute.instance.transform.position);
        Save save = CreateSaveGO();

        Debug.Log("SaveGame");
        ES3.Save<Save>("SaveGame",CreateSaveGO());

        //ES3.Save<Scene>
        
    }
    public void Load()
    {
        //LoadByJson();
        Save save = ES3.Load<Save>("SaveGame");
        Debug.Log("LoadGame");
        setGame(save);
        GlobalObject.Instance.LoadNewScene(save.sceneName);
    }

    /// <summary>
    /// 加载宝石位置
    /// </summary>
    /// <param name="list"></param>
    public void LoadDiamond(List<bool> list)
    {
        GameObject diamondRoot = GameObject.Find("DiamondCollections");
        Transform[] diamondAc = diamondRoot.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < list.Count; i++)
        {
            if (i==0)
            {
                continue;
            }
            diamondAc[i].gameObject.SetActive(list[i]);

        }
    }

    /// <summary>
    /// 加载可破坏墙壁位置
    /// </summary>
    /// <param name="list"></param>
    public void LoadDamagable(List<bool> list)
    {
        GameObject damageableWallRoot = GameObject.Find("DamageableWalls");
        Transform[] damageableAc = damageableWallRoot.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            damageableAc[i].gameObject.SetActive(list[i]);
        }
    }

    public void CreateGameManager()
    {
    }

}
