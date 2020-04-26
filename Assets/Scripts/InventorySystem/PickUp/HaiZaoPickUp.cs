using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaiZaoPickUp : MonoBehaviour
{
    private PickUpItem _pickUpItem;
    void Start()
    {
        _pickUpItem = GetComponent<PickUpItem>();
        _pickUpItem.onPick = OnPickUp;
    }

    private void OnPickUp(InventorySystem inventorySystem)
    {
        inventorySystem.AddItemAmount(ItemType.HaiZao,1);   
        UIManager.Instance.UpdateAllGamingUI();
    }
}
