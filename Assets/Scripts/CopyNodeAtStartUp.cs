using UnityEngine;
using System.Collections;

/// <summary>
/// initialize tree nodes.
/// </summary>
public class CopyNodeAtStartUp : MonoBehaviour
{
    public GameObject stepButton;
    public Transform nodePrefab;

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
                    GameObject lineNode = managerScript.lineNodes[index];// GameObject.Find(name);
                    Transform treeNode = Instantiate(nodePrefab) as Transform;
                    treeNode.position = lineNode.transform.position;
                    treeNode.name = Names.GetTreeNodeName(index);
                    treeNode.renderer.material = lineNode.GetComponentInChildren<Renderer>().material;
                    TextMesh textMesh = treeNode.GetComponentInChildren<TextMesh>();
                    textMesh.text = managerScript.targetList[index].ToString();

                    MoveInLine script = treeNode.gameObject.AddComponent<MoveInLine>();
                    script.startTime = startTime;
                    script.endTime = startTime + movingTimeFromLineNodeToTreeNode;
                    script.startPosition = lineNode.transform.position;
                    script.endPosition = child.position;

                    startTime += movingTimeFromLineNodeToTreeNode;

                    managerScript.treeNodes.Add(treeNode.gameObject);
                }
            }

            ShowButtonAtTime showAtTimeScrpt = stepButton.AddComponent<ShowButtonAtTime>();
            showAtTimeScrpt.showTime = startTime;

            initilized = true;
        }
    }
}
