using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public static UpdateUI instance;
    //private Slider bloodBar;
    //private GameObject bloodBarFill;
    public GameObject bloodCell;
    private Image fade;
    private Animation colorAnimation;
    public AnimationClip alpha;
    private Text collectionNum;
    [SerializeField]private Image[] bloodCells=new Image[6];
    private CanvasGroup pausedMenu;
    public CanvasGroup PausedMenu
    {
        get {
            if (pausedMenu==null)
            {
                pausedMenu = transform.Find("Menu").GetComponent<CanvasGroup>();
            }
            return pausedMenu; }
    }

    private void Awake()
    {
        instance = this;
        fade = transform.Find("Fade").GetComponent<Image>();
        colorAnimation = fade.gameObject.GetComponent<Animation>();
        if (pausedMenu==null)
        {
            pausedMenu = transform.Find("Menu").GetComponent<CanvasGroup>();
        }
        collectionNum = transform.Find("GamingUI/Collection/CollectionNum").GetComponent<Text>();
    }
    private void Start()
    {
        if (bloodCell==null)
        {
        bloodCell = transform.Find("GamingUI/BloodBar/BloodCell").gameObject;
            for (int i = 0; i < 6; i++)
            {
                bloodCells[i] = bloodCell.transform.Find(i.ToString()).GetComponent<Image>();
            }

        }
        PanelHide(pausedMenu);
        //UpdateAllUI(PlayerAttribute.instance.currentHp, PlayerAttribute.instance.maxHp, GlobalObject.Instance.GetIntValueInDictionary(PlayerAttribute.instance.collectionDic,Collection.ItemType.diamond));     
    }



    public void UpdateAllUI()
    {
        UpdateHpUI(PlayerManager.Instance.Health.currentHealth,
        PlayerManager.Instance.Health.maxHealth);
        UpdateDiamondCollection(InventorySystem.Instance
        .GetItemAmount(ItemType.Diamond));
    }
    //死亡遮挡UI
    public void FadeAnimation()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        colorAnimation.clip = alpha;
        colorAnimation.Play();
        yield return 1f;
    }
  
    //血量UI
    //public void UpdateHpUI(float currentHp,float maxHp)
    //{
    //    bloodBar.value = currentHp/maxHp;
    //}
    public void UpdateHpUI(float currentHp, float maxHp)
    {
        for (int i = 0; i < maxHp; i++)
        {
            if (i<currentHp)
            {
                bloodCells[i].enabled = true;
            }
            else
            {
                bloodCells[i].enabled = false;
            }
        }
    }

    //收集物UI
    public void UpdateDiamondCollection(int num)
    {
        collectionNum.text = num.ToString();
    }

    public void PanelShow(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void PanelHide(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
