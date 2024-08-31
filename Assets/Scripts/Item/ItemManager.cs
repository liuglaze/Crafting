using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemSO> Items;
    public ItemSO SearchItemByid(int id)
    {
        foreach (var item in Items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
