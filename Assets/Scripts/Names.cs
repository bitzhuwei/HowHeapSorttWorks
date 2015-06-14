using UnityEngine;
using System.Collections;

public class Names 
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
