using UnityEngine;
using System.Collections;

public class HideButtonAtStartup : MonoBehaviour
{

    private UnityEngine.UI.Text text;
    private UnityEngine.UI.Image image;

    // Use this for initialization
    void Start()
    {
        //this.renderer.enabled = false;
        this.image = this.GetComponent<UnityEngine.UI.Image>();
        this.image.enabled = false;
        this.text = this.GetComponentInChildren<UnityEngine.UI.Text>();
        this.text.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
