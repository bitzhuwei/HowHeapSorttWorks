using UnityEngine;
using System.Collections;

public class CopyNodeAtStartUp : MonoBehaviour
{
    public float movingTimeFromLineNodeToTreeNode = 0.5f;

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
            GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
            SortingManager managerScript = managerObj.GetComponent<SortingManager>();

            float startTime = Time.time;
            for (int index = 0; index < this.transform.childCount; index++)
            {
                Transform child = this.transform.FindChild(index.ToString());
                if (child != null)
                {
                    string name = Names.GetLineNodeName(index);
                    GameObject lineNode = managerScript.lineNodePositions[index];// GameObject.Find(name);
                    GameObject treeNode = Instantiate(lineNode) as GameObject;
                    treeNode.name = Names.GetTreeNodeName(index);
                    MoveFromLineNodeToTreeNode script = treeNode.AddComponent<MoveFromLineNodeToTreeNode>();
                    script.startTime = startTime;
                    script.endTime = startTime + movingTimeFromLineNodeToTreeNode;
                    script.startPosition = lineNode.transform.position;
                    script.endPosition = child.position;

                    startTime += movingTimeFromLineNodeToTreeNode;

                    managerScript.treeNodePositions.Add(treeNode.gameObject);
                }
            }

            initilized = true;
        }
    }
}
