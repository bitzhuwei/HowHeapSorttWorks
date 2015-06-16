using UnityEngine;
using System.Collections;

public class TreeNodeSwap : MonoBehaviour
{

    SortingManager manager;

    void Awake()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
        SortingManager managerScript = managerObj.GetComponent<SortingManager>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
