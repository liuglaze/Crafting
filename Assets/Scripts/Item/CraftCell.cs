using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CraftCell : MonoBehaviour, IPointerClickHandler
{
    public CraftingTable craftTable;
    public Image image;
    public TextMeshProUGUI text;
    public int index;

    public void SetItemStack(ItemStack stack)
    {
        if (stack == null || stack.itemSO == null)
        {
            image.sprite = null;
            text.text = "";
        }
        else
        {
            image.sprite = stack.itemSO.itemSprite;
            text.text = stack.quantity.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        // 如果当前有拖拽物品
        if (DragSystem.Instance.currentStack != null && DragSystem.Instance.currentStack.itemSO != null)
        {
            // 尝试与当前格子的物品交换
            var currentStack = craftTable.currentTable[index / 3, index % 3];
            craftTable.SetGrid(DragSystem.Instance.currentStack, index);
            SetItemStack(DragSystem.Instance.currentStack);
            DragSystem.Instance.currentStack = currentStack; // 拖拽系统持有格子的原有物品
        }
        else
        {
            // 如果当前没有拖拽物品，取出格子中的物品
            var currentStack = craftTable.currentTable[index / 3, index % 3];
            DragSystem.Instance.currentStack = currentStack;
            craftTable.ClearGrid(index);
            SetItemStack(null); // 更新UI为空
        }
    }

    private void OnRightClick()
    {
        var currentStack = craftTable.currentTable[index / 3, index % 3];

        if (DragSystem.Instance.currentStack != null && DragSystem.Instance.currentStack.itemSO != null)
        {
            if (currentStack != null && currentStack.itemSO == DragSystem.Instance.currentStack.itemSO)
            {
                // 拖拽物品与当前格子物品相同
                if (DragSystem.Instance.currentStack.quantity > 1)
                {
                    DragSystem.Instance.currentStack.quantity--;
                    currentStack.quantity++;
                }
                else
                {
                    currentStack.quantity++;
                    DragSystem.Instance.currentStack = null;
                }
            }
            else if (currentStack == null || currentStack.itemSO == null)
            {
                // 如果当前格子为空
                craftTable.SetGrid(new ItemStack(DragSystem.Instance.currentStack.itemSO, 1), index);
                DragSystem.Instance.currentStack.quantity--;

                if (DragSystem.Instance.currentStack.quantity == 0)
                {
                    DragSystem.Instance.currentStack = null;
                }
            }
        }
        else if (currentStack != null && currentStack.itemSO != null)
        {
            // 如果当前没有拖拽物品，从格子中取出一半的物品
            int halfQuantity = (currentStack.quantity + 1) / 2;
            DragSystem.Instance.currentStack = new ItemStack(currentStack.itemSO, halfQuantity);
            currentStack.quantity -= halfQuantity;

            if (currentStack.quantity == 0)
            {
                craftTable.ClearGrid(index);
            }
            SetItemStack(craftTable.currentTable[index / 3, index % 3]);
        }
    }
}


