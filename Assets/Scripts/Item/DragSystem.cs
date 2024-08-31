using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DragSystem : Singleton<DragSystem>
{
    public ItemStack currentStack; // ��ǰ����ק����Ʒ��
    public Image dragImage; // ������ʾ��Ʒͼ��� UI Image
    public TextMeshProUGUI dragImageAmount;

    private void Start()
    {
        // ȷ����קͼ���ڿ�ʼʱ�����ص�
        if (dragImage != null)
        {
            dragImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentStack != null && currentStack.itemSO != null)
        {
            // ��ʾ��קͼ�񲢸�����λ��
            dragImage.gameObject.SetActive(true);
            dragImage.sprite = currentStack.itemSO.itemSprite;
            dragImageAmount.text = currentStack.quantity.ToString();
            dragImage.transform.position = Input.mousePosition; // ��ͼ��λ����Ϊ���λ��
        }
        else
        {
            // ������קͼ��
            dragImage.gameObject.SetActive(false);
        }
    }
}

