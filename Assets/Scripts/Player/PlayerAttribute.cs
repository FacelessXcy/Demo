using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    static public PlayerAttribute instance;
    public int currentHp =6;
    public int maxHp = 6;
    public int energy=10;
    public int damage = 1;
    
    public bool isDead = false;
    public bool isDamaging = false;

    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {
        if (AudioManager.Instance.csol == AudioManager.ChangeSceneOrLoad.Load)
        {
            GameManager.Instance.LoadDiamond(GlobalObject.Instance.diamondsAct);
            GameManager.Instance.LoadDamagable(GlobalObject.Instance.damageableWallsAct);
        }
        //UpdateUI.instance.bloodCell = GameObject.Find("Canvas/BloodBar/BloodCell").gameObject;
        //for (int i = 0; i < 6; i++)
        //{
        //    UpdateUI.instance.bloodCells[i] = UpdateUI.instance.bloodCell.transform.Find(i.ToString()).GetComponent<Image>();
        //}
        GlobalObject.Instance.LoadData();
    }

}
