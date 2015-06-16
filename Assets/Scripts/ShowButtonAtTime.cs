using UnityEngine;
using System.Collections;

public class ShowButtonAtTime : MonoBehaviour
{
    public float showTime;
    private UnityEngine.UI.Text text;
    private UnityEngine.UI.Image image;
    private bool done = false;

    // Use this for initialization
    void Start()
    {
        //this.renderer.enabled = false;
        this.image = this.GetComponent<UnityEngine.UI.Image>();
        this.text = this.GetComponentInChildren<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (showTime <= Time.time)
            {
                this.image.enabled = true;
                this.text.enabled = true;
                this.done = true;
            }
        }
    }
}
