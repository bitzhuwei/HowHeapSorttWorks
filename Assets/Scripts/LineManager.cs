using UnityEngine;
using System.Collections;

public class LineManager : MonoBehaviour {

    public Transform nodePrefab;

	// Use this for initialization
	void Start () {
        if (nodePrefab != null)
        {
            GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
            SortingManager managerScript = managerObj.GetComponent<SortingManager>();

            float now = Time.time;
            for (int index = 0; index < this.transform.childCount; index++)
            {
                Transform child = this.transform.FindChild(index.ToString());
                if (child != null)
                {
                    if (int.TryParse(child.name, out index))
                    {
                        int number = Random.Range(0, 100);
                        Transform lineNode = Instantiate(nodePrefab) as Transform;
                        lineNode.position = child.position;
                        lineNode.GetComponentInChildren<TextMesh>().text = number.ToString();
                        lineNode.name = Names.GetLineNodeName(index);
                        lineNode.renderer.material = child.renderer.material;
                        lineNode.GetComponentInChildren<TextMesh>().renderer.enabled = false;
                        lineNode.renderer.enabled = false;
                        DelayShow script = lineNode.gameObject.AddComponent<DelayShow>();
                        script.showTime = index / 2f + now;
                        managerScript.lineNodes.Add(lineNode.gameObject);
                        managerScript.targetList.Add(number);
                    }
                }
            }
            //for (float i = -9, j = 0; i < 10; i += 1.2f, j++)
            //{
            //    Transform node = Instantiate(nodePrefab) as Transform;
            //    node.position = new Vector3(i, 0, 0);
            //    TextMesh textMesh = node.GetComponentInChildren<TextMesh>();
            //    textMesh.text = j.ToString();
            //    node.name = j.ToString();
            //}
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
