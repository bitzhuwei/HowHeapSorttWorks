using UnityEngine;
using System.Collections;

public static class IListHelper
{

    public static void SwapElement<T>(this System.Collections.Generic.IList<T> list, int p1, int p2)
    {
        T tmp = list[p1];
        list[p1] = list[p2];
        list[p2] = tmp;
    }
}
