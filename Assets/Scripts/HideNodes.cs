﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Hide nodes that provide original positions for moving line nodes and tree nodes.
/// </summary>
public class HideNodes : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            int tmp;
            if (int.TryParse(child.name, out tmp))
            {
                child.renderer.enabled = false;
                child.GetChild(0).renderer.enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
