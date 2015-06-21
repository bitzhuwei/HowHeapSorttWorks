using UnityEngine;
using System.Collections;

/// <summary>
/// Delay for some time to show the game object.
/// </summary>
public class DelayShow : MonoBehaviour
{
    public float showTime;
    bool done = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (showTime <= Time.time)
            {
                this.GetComponentInChildren<TextMesh>().renderer.enabled = true;
                Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
                foreach (var item in renderers)
                {
                    item.enabled = true;
                }
                //TextMesh[] textMeshes = this.GetComponentsInChildren<TextMesh>();
                //foreach (var item in textMeshes)
                //{
                //    item.renderer.enabled = true;
                //}

                //{
                //    Transform valueNode = this.transform.Find("Value");
                //    valueNode.renderer.enabled = true;
                //    TextMesh textMesh = valueNode.GetComponentInChildren<TextMesh>();
                //    textMesh.renderer.enabled = true;
                //}
                //{
                //    Transform indexNode = this.transform.Find("Index");
                //    indexNode.renderer.enabled = true;
                //    TextMesh textMesh = indexNode.GetComponentInChildren<TextMesh>();
                //    textMesh.renderer.enabled = true;
                //}

                done = true;
            }
        }
    }
}
