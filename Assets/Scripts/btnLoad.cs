using UnityEngine;
using System.Collections;

public class btnLoad : MonoBehaviour
{
    public Transform nodePrefab;
    public UnityEngine.UI.InputField txtArray;
    public UnityEngine.UI.Text txtArrayDataError;
    public Transform lineManager;
    public Transform treeManager;
    public float movingTimeFromLineNodeToTreeNode = 0.5f;

    private SortingManager sortingManager;
    private bool done = true;
    private System.Collections.Generic.List<int> array;
    // Use this for initialization
    void Start()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
        this.sortingManager = managerObj.GetComponent<SortingManager>();
        if(array == null)
        {
            array = new System.Collections.Generic.List<int>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done) { return; }

        this.sortingManager.stop = true;
        GameObject[] nodes = GameObject.FindGameObjectsWithTag(Tags.NodePrefab);
        foreach (var item in nodes)
        {
            Destroy(item);
        }

        System.Collections.Generic.List<int> targetList = new System.Collections.Generic.List<int>();
        System.Collections.Generic.List<GameObject> lineNodes = new System.Collections.Generic.List<GameObject>();

        float now = Time.time;

        for (int index = 0; index < this.lineManager.transform.childCount && index < this.array.Count; index++)
        {
            Transform child = this.lineManager.transform.FindChild(index.ToString());
            if (child != null)
            {
                if (int.TryParse(child.name, out index))
                {
                    Transform lineNode = Instantiate(nodePrefab) as Transform;
                    lineNode.position = child.position;
                    lineNode.GetComponentInChildren<TextMesh>().text = this.array[index].ToString();
                    lineNode.name = Names.GetLineNodeName(index);
                    lineNode.renderer.material = child.renderer.material;
                    lineNode.GetComponentInChildren<TextMesh>().renderer.enabled = false;
                    lineNode.renderer.enabled = false;
                    DelayShow script = lineNode.gameObject.AddComponent<DelayShow>();
                    script.showTime = index / 2f + now;
                    lineNodes.Add(lineNode.gameObject);
                    targetList.Add(this.array[index]);
                }
            }
        }

        System.Collections.Generic.List<GameObject> treeNodes = new System.Collections.Generic.List<GameObject>();
        float startTime = Time.time;

        for (int index = 0; index < this.treeManager.transform.childCount && index < this.array.Count; index++)
        {
            Transform child = this.treeManager.transform.FindChild(index.ToString());
            if (child != null)
            {
                string name = Names.GetLineNodeName(index);
                GameObject lineNode = lineNodes[index];// GameObject.Find(name);
                GameObject treeNode = Instantiate(lineNode) as GameObject;
                treeNode.name = Names.GetTreeNodeName(index);
                MoveInLine script = treeNode.AddComponent<MoveInLine>();
                script.startTime = startTime;
                script.endTime = startTime + movingTimeFromLineNodeToTreeNode;
                script.startPosition = lineNode.transform.position;
                script.endPosition = child.position;

                startTime += movingTimeFromLineNodeToTreeNode;

                treeNodes.Add(treeNode.gameObject);
            }
        }


        this.sortingManager.targetList = targetList;
        this.sortingManager.lineNodes = lineNodes;
        this.sortingManager.treeNodes = treeNodes;

        this.done = true;
        this.sortingManager.stop = false;
    }

    static readonly char[] separator = new char[] { ',', ' ' };

    public void btnLoad_Click()
    {
        bool noError = true;
        this.array.Clear();
        string[] numbers = this.txtArray.text.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < numbers.Length && i < 15; i++)
        {
            int number;
            if (int.TryParse(numbers[i],out number))
            {
                this.array.Add(number);
            }
            else
            {
                this.txtArrayDataError.text = string.Format("Wrong number: {0}", numbers[i]);
                noError = false;
                break;
            }
        }

        if(noError)
        {
            this.txtArrayDataError.text = string.Empty;
            this.done = false;
        }

    }
}
