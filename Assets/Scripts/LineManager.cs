using UnityEngine;
using System.Collections;

/// <summary>
/// initialize <see cref="SortingManager"/>'s line node and target list.
/// </summary>
public class LineManager : MonoBehaviour {

    public Transform nodePrefab;
    public UnityEngine.UI.InputField txtArray;

	// Use this for initialization
	void Start () {
        if (nodePrefab != null)
        {
            GameObject managerObj = GameObject.FindGameObjectWithTag(Tags.SortingManager);
            SortingManager managerScript = managerObj.GetComponent<SortingManager>();

            float now = Time.time;
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            for (int index = 0; index < this.transform.childCount; index++)
            {
                Transform child = this.transform.FindChild(index.ToString());
                if (child != null)
                {
                    if (int.TryParse(child.name, out index))
                    {
                        int number = Random.Range(0, 100);
                        builder.Append(number);builder.Append(", ");
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

            this.txtArray.text = builder.ToString();
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
