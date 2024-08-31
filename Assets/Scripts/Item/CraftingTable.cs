using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public ItemStack[,] currentTable = new ItemStack[3, 3];
    public ResultCell resultCell;
    public CraftCell[] craftCells;
    public void SetGrid(ItemStack itemStack, int index)
    {
        currentTable[index / 3, index % 3] = itemStack;
        UpdateResult(); // ÿ�θ��¸��Ӻ���ϳɽ��
    }

    public void ClearGrid(int index)
    {
        currentTable[index / 3, index % 3] = null;
        UpdateCraftCell(index, null); // Update UI for CraftCell
        UpdateResult(); // ÿ����ո��Ӻ���ϳɽ��
    }

    public ItemStack CheckRecipe()
    {
        return CraftingSystem.Instance.CheckRecipe(CompressionTable());
    }

    private void UpdateResult()
    {
        var result = CheckRecipe();
        resultCell.SetItemStack(result); // ���ºϳɽ����ʾ
    }

    private ItemStack[,] CompressionTable()
    {
        // ��ȡ���ķǿ�����߽�
        int minX = 3, minY = 3, maxX = -1, maxY = -1;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (currentTable[x, y] != null && currentTable[x, y].itemSO != null)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }
        }

        if (minX > maxX || minY > maxY)
        {
            // ��ʾ���Ϊ��
            return new ItemStack[0, 0];
        }

        // ������С���Ķ�ά����
        int rows = maxX - minX + 1;
        int columns = maxY - minY + 1;
        ItemStack[,] minimizedTable = new ItemStack[rows, columns];

        // �����С��������
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                minimizedTable[x - minX, y - minY] = currentTable[x, y];
            }

        }

        return minimizedTable;
    }
    private void UpdateCraftCell(int index, ItemStack itemStack)
    {
        if (craftCells != null && index >= 0 && index < craftCells.Length)
        {
            craftCells[index].SetItemStack(itemStack);
        }
    }
    public void ClearCraftCells()
    {
        for (int i = 0; i < 9; i++) // Assuming 3x3 grid
        {
            ClearGrid(i);
        }
    }
}

