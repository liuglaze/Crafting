using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultCell : MonoBehaviour,IPointerClickHandler
{
    public CraftingTable craftTable;
    public Image image;
    public TextMeshProUGUI text;
    public ItemStack currentStack;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (currentStack != null)
            {
                DragSystem.Instance.currentStack = currentStack;
                craftTable.ClearCraftCells();
            }
        }
        
    }

    public void SetItemStack(ItemStack stack)
    {
        if(stack == null)
        {
            image.sprite = null;
            text.text = "";
            currentStack = null;
        }
        else
        {
            image.sprite = stack.itemSO.itemSprite;
            text.text=stack.quantity.ToString();
            currentStack = stack;
        }
    }
}
