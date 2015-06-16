using UnityEngine;
using System.Collections;

public class btnStep : MonoBehaviour
{
    private SortingManager sortingManager;
    private UnityEngine.UI.Text text;

    // Use this for initialization
    void Start()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
        this.sortingManager = managerObj.GetComponent<SortingManager>();

        this.text = this.GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnStep_Click()
    {
        this.sortingManager.IncreaseStep();
        this.text.text = this.sortingManager.GetNextStepName();
    }
}
