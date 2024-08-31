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
        // 初始化配方字典
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


    // 比较两个二维数组是否相等
    private bool AreGridsEqual(ItemStack[,] grid1, ItemStack[,] grid2)
    {
        // 检查数组是否为 null 或尺寸是否不同
        if (grid1 == null || grid2 == null ||
            grid1.GetLength(0) != grid2.GetLength(0) ||
            grid1.GetLength(1) != grid2.GetLength(1))
        {
            Debug.Log("尺寸不同或有一个数组为 null");
            return false; // 不同尺寸的数组直接返回 false
        }

        for (int i = 0; i < grid1.GetLength(0); i++)
        {
            for (int j = 0; j < grid1.GetLength(1); j++)
            {
                var item1 = grid1[i, j];
                var item2 = grid2[i, j];

                // 输出每个位置的内容
                Debug.Log($"位置 ({i}, {j}): grid1 - itemSO: {(item1?.itemSO?.name ?? "null")}");
                Debug.Log($"位置 ({i}, {j}): grid2 - itemSO: {(item2?.itemSO?.name ?? "null")}");

                if (item1?.itemSO == item2?.itemSO) continue; // itemSO 相同，视为相等

                if (item1 == null || item2 == null)
                {
                    // 详细日志记录
                    if (item1 == null && item2 != null)
                    {
                        Debug.Log($"位置 ({i}, {j}): grid1 为 null，grid2 不为 null");
                    }
                    else if (item1 != null && item2 == null)
                    {
                        Debug.Log($"位置 ({i}, {j}): grid1 不为 null，grid2 为 null");
                    }
                    else if (item1 == null && item2 == null)
                    {
                        Debug.Log($"位置 ({i}, {j}): grid1 和 grid2 都为 null");
                    }
                    return false; // 一个为 null，另一个不为 null
                }

                // 比较 itemSO
                if (!item1.itemSO.Equals(item2.itemSO))
                {
                    Debug.Log($"位置 ({i}, {j}): itemSO 不同 - grid1: {item1.itemSO?.name ?? "null"} vs grid2: {item2.itemSO?.name ?? "null"}");
                    return false; // itemSO 不同
                }
            }
        }

        Debug.Log("两个数组完全相同");
        return true; // 数组内容完全相同
    }


    private void PrintGridContents(string gridName, ItemStack[,] grid)
    {
        Debug.Log($"{gridName} 内容:");
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

