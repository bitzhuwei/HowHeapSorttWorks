using UnityEngine;
using System.Collections;

public class btnStep : MonoBehaviour
{
    SortingManager sortingManager;

    // Use this for initialization
    void Start()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
        this.sortingManager = managerObj.GetComponent<SortingManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnStep_Click()
    {
        this.sortingManager.IncreaseStep(); 
    }
}
