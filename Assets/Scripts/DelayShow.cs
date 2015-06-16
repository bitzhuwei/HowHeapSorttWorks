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
                this.renderer.enabled = true;
                this.GetComponentInChildren<TextMesh>().renderer.enabled = true;

                done = true;
            }
        }
    }
}
