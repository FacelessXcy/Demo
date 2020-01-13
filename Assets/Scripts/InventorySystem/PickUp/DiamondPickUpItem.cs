using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickUpItem : MonoBehaviour
{
    public int amount;
    private PickUpItem _pickUpItem;
    void Start()
    {
        _pickUpItem = GetComponent<PickUpItem>();
    }


    private void OnPickUp(InventorySystem inventorySystem)
    {
        InventorySystem.Instance.AddAddItemAmount(,int amount)
    }

}
