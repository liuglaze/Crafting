using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
[Serializable]
public class ItemStack 
{
    public ItemSO itemSO;
    public int quantity=0;
    public ItemStack(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
    }

    public bool Equals(ItemStack other)
    {
        return other != null && itemSO.id == other.itemSO.id && quantity == other.quantity;
    }
}
