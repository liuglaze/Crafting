using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DragSystem : Singleton<DragSystem>
{
    public ItemStack currentStack; // 当前被拖拽的物品堆
    public Image dragImage; // 用于显示物品图像的 UI Image
    public TextMeshProUGUI dragImageAmount;

    private void Start()
    {
        // 确保拖拽图像在开始时是隐藏的
        if (dragImage != null)
        {
            dragImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentStack != null && currentStack.itemSO != null)
        {
            // 显示拖拽图像并更新其位置
            dragImage.gameObject.SetActive(true);
            dragImage.sprite = currentStack.itemSO.itemSprite;
            dragImageAmount.text = currentStack.quantity.ToString();
            dragImage.transform.position = Input.mousePosition; // 将图像位置设为鼠标位置
        }
        else
        {
            // 隐藏拖拽图像
            dragImage.gameObject.SetActive(false);
        }
    }
}

