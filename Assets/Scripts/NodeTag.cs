using UnityEngine;
using System.Collections;

public class NodeTag : MonoBehaviour {

    void Awake()
    {
        this.gameObject.tag = Tags.NodePrefab;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
