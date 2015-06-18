using UnityEngine;
using System.Collections;

public class btnRandomArray : MonoBehaviour
{
    public UnityEngine.UI.InputField txtArrayText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnRandomArray_Click()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        for (int i = 0; i < 15; i++)
        {
            int number = Random.Range(0, 100);
            builder.Append(number);
            builder.Append(", ");
        }

        this.txtArrayText.text = builder.ToString();
    }
}
