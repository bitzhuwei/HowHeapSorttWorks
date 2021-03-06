﻿using UnityEngine;
using System.Collections;

public class btnLoad : MonoBehaviour
{
    public Transform indexNodePrefab;
    public Transform nodePrefab;
    public UnityEngine.UI.InputField txtArray;
    public UnityEngine.UI.Text txtArrayDataError;
    public UnityEngine.UI.Text btnStepText;

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
        for (int i = 0; i < 15; i++)
        {

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
                    Transform lineNode = Instantiate(indexNodePrefab) as Transform;
                    lineNode.position = child.position;
                    lineNode.name = Names.GetLineNodeName(index);
                    {
                        Transform valueNode = lineNode.Find("Value");
                        valueNode.renderer.material = child.renderer.material;
                        valueNode.renderer.enabled = false;
                        TextMesh textMesh = valueNode.GetComponentInChildren<TextMesh>();
                        textMesh.text = this.array[index].ToString();
                        textMesh.renderer.enabled = false;
                    }
                    {
                        Transform indexNode = lineNode.Find("Index");
                        indexNode.renderer.material = child.renderer.material;
                        indexNode.renderer.enabled = false;
                        TextMesh textMesh = indexNode.GetComponentInChildren<TextMesh>();
                        textMesh.text = index.ToString();
                        textMesh.renderer.enabled = false;
                    }

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
                //string name = Names.GetLineNodeName(index);
                //GameObject lineNode = lineNodes[index];// GameObject.Find(name);
                //GameObject treeNode = Instantiate(lineNode) as GameObject;
                //treeNode.name = Names.GetTreeNodeName(index);
                //MoveInLine script = treeNode.AddComponent<MoveInLine>();
                //script.startTime = startTime;
                //script.endTime = startTime + movingTimeFromLineNodeToTreeNode;
                //script.startPosition = lineNode.transform.position;
                //script.endPosition = child.position;
                //
                string name = Names.GetLineNodeName(index);
                GameObject lineNode = lineNodes[index];// GameObject.Find(name);
                Transform treeNode = Instantiate(nodePrefab) as Transform;
                treeNode.position = lineNode.transform.position;
                treeNode.name = Names.GetTreeNodeName(index);
                {
                    treeNode.renderer.material = lineNode.GetComponentInChildren<Renderer>().material;
                    treeNode.renderer.enabled = false;
                    TextMesh textMesh = treeNode.GetComponentInChildren<TextMesh>();
                    textMesh.text = targetList[index].ToString();
                    textMesh.renderer.enabled = false;
                }
                {
                    DelayShow script = treeNode.gameObject.AddComponent<DelayShow>();
                    script.showTime = lineNode.GetComponent<DelayShow>().showTime;
                }
                {
                    MoveInLine script = treeNode.gameObject.AddComponent<MoveInLine>();
                    script.startTime = startTime;
                    script.endTime = startTime + movingTimeFromLineNodeToTreeNode;
                    script.startPosition = treeNode.position;
                    script.endPosition = child.position;
                }

                startTime += movingTimeFromLineNodeToTreeNode;

                treeNodes.Add(treeNode.gameObject);
            }
        }


        ShowButtonAtTime showAtTimeScrpt = this.btnStepText.GetComponentInParent<ShowButtonAtTime>();
        if(showAtTimeScrpt==null)
        { showAtTimeScrpt = this.btnStepText.transform.parent.gameObject.AddComponent<ShowButtonAtTime>(); }
        showAtTimeScrpt.showTime = startTime;
        showAtTimeScrpt.GetComponent<UnityEngine.UI.Image>().enabled = false;
        this.btnStepText.enabled = false;

        this.sortingManager.targetList = targetList;
        this.sortingManager.lineNodes = lineNodes;
        this.sortingManager.treeNodes = treeNodes;

        this.done = true;
        this.sortingManager.stop = false;
        this.sortingManager.Reset();
        this.btnStepText.text = this.sortingManager.GetNextStepName();
        
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
