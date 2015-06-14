using UnityEngine;
using System.Collections;

public class CopyNodeAtStartUp : MonoBehaviour
{
    bool initilized = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initilized)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                int index;
                if (int.TryParse(child.name, out index))
                {
                    string name = Names.GetLineNodeName(index);
                    GameObject lineNode = GameObject.Find(name);
                    GameObject treeNode = Instantiate(lineNode) as GameObject;
                    treeNode.name = Names.GetTreeNodeName(index);
                }
            }

            initilized = true;
        }
    }
}
