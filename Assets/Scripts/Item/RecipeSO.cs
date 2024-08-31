using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class RecipeSO : ScriptableObject
{
    [Title("Crafting Grid Dimensions")]
    [MinValue(1)]
    [LabelText("Rows")]
    public int rows = 1;

    [MinValue(1)]
    [LabelText("Columns")]
    public int columns = 1;

    [Title("Crafting Grid")]
    [TableMatrix(HorizontalTitle = "Item Grid", SquareCells = false, ResizableColumns = true, RowHeight = 500, Transpose = true)]
    [PropertySpace(10)]
    [ShowInInspector]
    public ItemStack[] grid;

    [Button(ButtonSizes.Large)]
    [LabelText("Adjust Grid Size")]
    private void AdjustGridSize()
    {
        grid = new ItemStack[rows * columns];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new ItemStack(null, 0);
        }
    }

    public ItemStack reward;
    public ItemStack GetItemStack(int row, int column)
    {
        if (row >= 0 && row < rows && column >= 0 && column < columns)
        {
            return grid[row * columns + column];
        }
        return null;
    }
    public ItemStack[,] ToMinimizedGrid()
    {
        ItemStack[,] minimizedGrid = new ItemStack[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                minimizedGrid[i, j] = GetItemStack(i, j);
            }
        }
        return minimizedGrid;
    }

}






