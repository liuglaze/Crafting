using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : Singleton<CraftingSystem>
{
    public List<RecipeSO> recipes;
    public Dictionary<ItemStack[,], ItemStack> recipesDict;

    override public void Awake()
    {
        base.Awake();
        recipesDict = new Dictionary<ItemStack[,], ItemStack>();
    }

    private void Start()
    {
        // ��ʼ���䷽�ֵ�
        foreach (var recipe in recipes)
        {
            recipesDict[recipe.ToMinimizedGrid()] = recipe.reward;
        }
    }

    public ItemStack CheckRecipe(ItemStack[,] craftTable)
    {
        foreach (var recipe in recipesDict)
        {
            if (AreGridsEqual(recipe.Key, craftTable))
            {
                return recipe.Value;
            }
        }
        return null;
    }


    // �Ƚ�������ά�����Ƿ����
    private bool AreGridsEqual(ItemStack[,] grid1, ItemStack[,] grid2)
    {
        // ��������Ƿ�Ϊ null ��ߴ��Ƿ�ͬ
        if (grid1 == null || grid2 == null ||
            grid1.GetLength(0) != grid2.GetLength(0) ||
            grid1.GetLength(1) != grid2.GetLength(1))
        {
            Debug.Log("�ߴ粻ͬ����һ������Ϊ null");
            return false; // ��ͬ�ߴ������ֱ�ӷ��� false
        }

        for (int i = 0; i < grid1.GetLength(0); i++)
        {
            for (int j = 0; j < grid1.GetLength(1); j++)
            {
                var item1 = grid1[i, j];
                var item2 = grid2[i, j];

                // ���ÿ��λ�õ�����
                Debug.Log($"λ�� ({i}, {j}): grid1 - itemSO: {(item1?.itemSO?.name ?? "null")}");
                Debug.Log($"λ�� ({i}, {j}): grid2 - itemSO: {(item2?.itemSO?.name ?? "null")}");

                if (item1?.itemSO == item2?.itemSO) continue; // itemSO ��ͬ����Ϊ���

                if (item1 == null || item2 == null)
                {
                    // ��ϸ��־��¼
                    if (item1 == null && item2 != null)
                    {
                        Debug.Log($"λ�� ({i}, {j}): grid1 Ϊ null��grid2 ��Ϊ null");
                    }
                    else if (item1 != null && item2 == null)
                    {
                        Debug.Log($"λ�� ({i}, {j}): grid1 ��Ϊ null��grid2 Ϊ null");
                    }
                    else if (item1 == null && item2 == null)
                    {
                        Debug.Log($"λ�� ({i}, {j}): grid1 �� grid2 ��Ϊ null");
                    }
                    return false; // һ��Ϊ null����һ����Ϊ null
                }

                // �Ƚ� itemSO
                if (!item1.itemSO.Equals(item2.itemSO))
                {
                    Debug.Log($"λ�� ({i}, {j}): itemSO ��ͬ - grid1: {item1.itemSO?.name ?? "null"} vs grid2: {item2.itemSO?.name ?? "null"}");
                    return false; // itemSO ��ͬ
                }
            }
        }

        Debug.Log("����������ȫ��ͬ");
        return true; // ����������ȫ��ͬ
    }


    private void PrintGridContents(string gridName, ItemStack[,] grid)
    {
        Debug.Log($"{gridName} ����:");
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var item = grid[i, j];
                row += $"({i}, {j}): {item?.itemSO?.name ?? "null"}, {item?.quantity ?? 0} | ";
            }
            Debug.Log(row);
        }
    }
}

