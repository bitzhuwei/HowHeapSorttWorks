using UnityEngine;
using System.Collections;

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

                done = true;
            }
        }
    }
}
