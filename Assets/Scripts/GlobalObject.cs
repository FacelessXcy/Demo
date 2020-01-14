using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalObject : MonoBehaviour
{
    static GlobalObject instance;
    public string sceneName;
    public int sceneIndex;

    public  int maxHp =6;
    public int currentHp =6;
    //public Dictionary<Collection.ItemType, int> collectionDic=new Dictionary<Collection.ItemType, int>();
    public int energy=0;
    public int damage=1;
    [SerializeField]private Vector3 savePlayerPos;
    [SerializeField]private Vector3 nextScenePos=new Vector3(-176.7f,-222.9f,0);
   // public bool isChangeScene = false;

    //保存怪物的位置
    //public List<Vector2> monstersPos = new List<Vector2>();
    //保存宝石的Active
    public List<bool> diamondsAct = new List<bool>();
    //保存未破坏墙壁的Active
    public List<bool> damageableWallsAct = new List<bool>();

    static public GlobalObject Instance
    {
        get
        {
            if (instance==null)
            {
                instance = Object.FindObjectOfType(typeof(GlobalObject))as GlobalObject;
                if (instance==null)
                {
                    GameObject go = new GameObject("GlobalObject");
                    DontDestroyOnLoad(go); 
                    instance = go.AddComponent<GlobalObject>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        GameManager.Instance.CreateGameManager();
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Awake执行");
        //Debug.Log(collectionDic);
    }
   

    private void Update()
    {
        //Debug.Log(GameManager.Instance.name);
        //Debug.Log(SceneManager.GetActiveScene().name+"当前场景");
        //Debug.Log(this.savePlayerPos+"当前保存玩家位置");
    }


    public void LoadData()//加载场景后，载入数据
    {
        //Debug.Log(GlobalObject.instance);
        if (PlayerAttribute.instance != null)
        {
            PlayerAttribute.instance.maxHp = this.maxHp;
            PlayerAttribute.instance.currentHp = this.currentHp;
            PlayerAttribute.instance.damage = this.damage;
            //PlayerManager.Instance.collectionDic = this.collectionDic;
            PlayerAttribute.instance.energy = this.energy;
            if (AudioManager.Instance.csol==AudioManager.ChangeSceneOrLoad.Load)
            {
                PlayerAttribute.instance.transform.position = this.savePlayerPos;//载入存档时进行场景切换
            }
            if (AudioManager.Instance.csol== AudioManager.ChangeSceneOrLoad.ChangeScene)
            {
                PlayerAttribute.instance.transform.position = this.nextScenePos;//通过传送点进行场景切换
            }
            //否则采用场景默认位置
            //UpdateUI.instance.UpdateAllUI();
            UIManager.Instance.UpdateAllGamingUI();
        }
    }

    public void SaveData()//切换场景时，保存数据
    {
        if (PlayerAttribute.instance != null)
        {
            //Debug.Log(SceneManager.GetActiveScene().name);
            if (AudioManager.Instance.csol==AudioManager.ChangeSceneOrLoad.Load)//载入存档时，save对象已经给GlobalObject赋值，不用再次赋值
            {
                return;
            }
            else//切换场景时，需要保存当前角色状态信息
            {                
                this.maxHp = PlayerAttribute.instance.maxHp;
                this.currentHp = PlayerAttribute.instance.currentHp;
                this.damage = PlayerAttribute.instance.damage;
                //this.collectionDic = PlayerManager.Instance.collectionDic;
                this.energy = PlayerAttribute.instance.energy;
            }

            //Debug.Log(this.savePlayerPos);
            //Debug.Log(this.currentHp);
            //Debug.Log(this.maxHp);
            //Debug.Log(this.damage);
            //Debug.Log(this.collectionDic);
            //Debug.Log(this.energy);
        }
    }

    public void LoadNewScene(string sceneName)
    {
        //保存场景数据
        this.sceneName = sceneName;
        //Debug.Log(savePlayerPos+"切换场景到Loading");
        //保存游戏角色数据
        SaveData();
        SceneManager.LoadScene(1);
        //Debug.Log("Global场景切换函数"); 
    }

    public Vector3 GetNextScenePos()
    {
        return nextScenePos;
    }
    public void SetNextScenePos(Vector3 vector3)
    {
        this.nextScenePos = vector3;
    }

    public Vector3 GetSavePlayerPos()
    {
        return savePlayerPos;
    }

    public void SetSavePlayerPos(Vector3 vector)
    {
        this.savePlayerPos = vector;
    }

}
