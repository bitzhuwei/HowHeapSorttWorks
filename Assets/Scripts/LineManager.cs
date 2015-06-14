using UnityEngine;
using System.Collections;

public class LineManager : MonoBehaviour {

    public Transform nodePrefab;

	// Use this for initialization
	void Start () {
        if (nodePrefab != null)
        {
            for (float i = -9, j = 0; i < 10; i += 1.2f, j++)
            {
                Transform node = Instantiate(nodePrefab) as Transform;
                node.position = new Vector3(i, 0, 0);
                TextMesh textMesh = node.GetComponentInChildren<TextMesh>();
                textMesh.text = j.ToString();
                node.name = j.ToString();
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
