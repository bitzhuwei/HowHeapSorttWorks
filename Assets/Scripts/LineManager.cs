using UnityEngine;
using System.Collections;

public class LineManager : MonoBehaviour {

    public Transform nodePrefab;

	// Use this for initialization
	void Start () {
        if (nodePrefab != null)
        {
            float now = Time.time;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                int index;
                if (int.TryParse(child.name, out index))
                {
                    Transform lineNode = Instantiate(nodePrefab) as Transform;
                    lineNode.position = child.position;
                    lineNode.GetComponentInChildren<TextMesh>().text = Random.Range(0, 100).ToString();
                    lineNode.name = Names.GetLineNodeName(index);
                    lineNode.renderer.enabled = false;
                    DelayShow script = lineNode.gameObject.AddComponent<DelayShow>();
                    script.showTime = index / 2f + now;
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
