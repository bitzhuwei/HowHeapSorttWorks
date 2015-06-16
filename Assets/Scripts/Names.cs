using UnityEngine;
using System.Collections;

/// <summary>
/// Helps to get line node and tree nodes name in the scene.
/// </summary>
public class Names
{
    public static string GetLineNodeName(int index)
    {
        string name = string.Format("line {0}", index);
        return name;
    }

    public static string GetTreeNodeName(int index)
    {
        string name = string.Format("tree node {0}", index);
        return name;
    }
}
