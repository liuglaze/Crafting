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
        // �����ǰ����ק��Ʒ
        if (DragSystem.Instance.currentStack != null && DragSystem.Instance.currentStack.itemSO != null)
        {
            // �����뵱ǰ���ӵ���Ʒ����
            var currentStack = craftTable.currentTable[index / 3, index % 3];
            craftTable.SetGrid(DragSystem.Instance.currentStack, index);
            SetItemStack(DragSystem.Instance.currentStack);
            DragSystem.Instance.currentStack = currentStack; // ��קϵͳ���и��ӵ�ԭ����Ʒ
        }
        else
        {
            // �����ǰû����ק��Ʒ��ȡ�������е���Ʒ
            var currentStack = craftTable.currentTable[index / 3, index % 3];
            DragSystem.Instance.currentStack = currentStack;
            craftTable.ClearGrid(index);
            SetItemStack(null); // ����UIΪ��
        }
    }

    private void OnRightClick()
    {
        var currentStack = craftTable.currentTable[index / 3, index % 3];

        if (DragSystem.Instance.currentStack != null && DragSystem.Instance.currentStack.itemSO != null)
        {
            if (currentStack != null && currentStack.itemSO == DragSystem.Instance.currentStack.itemSO)
            {
                // ��ק��Ʒ�뵱ǰ������Ʒ��ͬ
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
                // �����ǰ����Ϊ��
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
            // �����ǰû����ק��Ʒ���Ӹ�����ȡ��һ�����Ʒ
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


