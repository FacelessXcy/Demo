using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemType
{
    HealItem,
    Diamond,
    DingXiangYu,
    HaiZao
}
public class InventorySystem : MonoSingleton<InventorySystem>
{
    
    private Dictionary<ItemType,int> _itemDic=new Dictionary<ItemType, 
    int>();
    
    public override void Awake()
    {
        base.Awake();
        InitDictionary();
    }

    private void InitDictionary()
    {
        if (!_itemDic.ContainsKey(ItemType.HealItem))
        {
            _itemDic.Add(ItemType.HealItem,0);
        }

        if (!_itemDic.ContainsKey(ItemType.Diamond))
        {
            _itemDic.Add(ItemType.Diamond,0);
        }
        if (!_itemDic.ContainsKey(ItemType.DingXiangYu))
        {
            _itemDic.Add(ItemType.DingXiangYu,0);
        }
        if (!_itemDic.ContainsKey(ItemType.HaiZao))
        {
            _itemDic.Add(ItemType.HaiZao,0);
        }
    }
    
    /// <summary>
    /// 获取相应物品种类的数量
    /// </summary>
    /// <param name="itemType">物品种类</param>
    /// <returns>数量</returns>
    public int GetItemAmount(ItemType itemType)
    {
        if (!_itemDic.ContainsKey(itemType))
        {
            _itemDic.Add(itemType,0);
        }
        return _itemDic[itemType];
    }

    public void AddItemAmount(ItemType itemType,int amount)
    {
        if (!_itemDic.ContainsKey(itemType))
        {
            _itemDic.Add(itemType,0);
        }
        _itemDic[itemType] += amount;
    }

    public void RemoveItemAmount(ItemType itemType,int amount)
    {
        if (!_itemDic.ContainsKey(itemType))
        {
            _itemDic.Add(itemType,0);
        }

        if (GetItemAmount(itemType)<amount)
        {
            _itemDic[itemType] = 0;
            return;
        }
        _itemDic[itemType] -= amount;
    }

}
