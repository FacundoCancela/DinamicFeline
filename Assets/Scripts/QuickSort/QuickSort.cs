using System;
using System.Collections.Generic;

public class QuickSort
{
    public static void Sort(List<Puntajes> list)
    {
        if (list == null || list.Count <= 1)
            return;

        QuickSortAlgorithm(list, 0, list.Count - 1);
    }

    private static void QuickSortAlgorithm(List<Puntajes> list, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right);
            QuickSortAlgorithm(list, left, pivotIndex - 1);
            QuickSortAlgorithm(list, pivotIndex + 1, right);
        }
    }

    private static int Partition(List<Puntajes> list, int left, int right)
    {
        Puntajes pivot = list[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (list[j].Puntaje >= pivot.Puntaje)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private static void Swap(List<Puntajes> list, int i, int j)
    {
        Puntajes temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
