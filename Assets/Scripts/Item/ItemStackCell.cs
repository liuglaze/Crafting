using UnityEngine;
using UnityEngine.EventSystems;

public class ItemStackCell : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (DragSystem.Instance.currentStack == null)
        {
            DragSystem.Instance.currentStack = new ItemStack(itemSO, 1);
        }
        else
        {
            if (DragSystem.Instance.currentStack.itemSO == itemSO)
            {
                DragSystem.Instance.currentStack.quantity++;
            }
            else
            {
                DragSystem.Instance.currentStack.itemSO = itemSO;
                DragSystem.Instance.currentStack.quantity = 1;
            }
        }
    }
}

