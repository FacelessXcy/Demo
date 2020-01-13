using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    private new AudioSource audio;

    public ItemType itemType;
    private void Awake()
    {
        transform.GetComponent<CircleCollider2D>().isTrigger = true;
        audio = GetComponent<AudioSource>();
    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        switch (itemType)
//        {
//            case ItemType.HealItem:
//                int a = GlobalObject.Instance.GetIntValueInDictionary
//                (PlayerManager.Instance.collectionDic, ItemType.HealItem);
//                GlobalObject.Instance.SetIntValueInDictionary(PlayerManager.Instance.collectionDic, ItemType.HealItem, a + 1);
//                //UpdateUI.instance.UpdateDiamondCollection(a+1);
//                //audio.Play();
//                gameObject.SetActive(false);
//                break;
//            case ItemType.Diamond:
//                int b = GlobalObject.Instance.GetIntValueInDictionary(PlayerManager.Instance.collectionDic, ItemType.Diamond);
//                //audio.Play();
//                GlobalObject.Instance.SetIntValueInDictionary(PlayerManager.Instance.collectionDic, ItemType.Diamond, b + 1);
//                UpdateUI.instance.UpdateDiamondCollection(b + 1);
//                gameObject.SetActive(false);
//                break;
//            default:
//                break;
//        }
//    }

}
