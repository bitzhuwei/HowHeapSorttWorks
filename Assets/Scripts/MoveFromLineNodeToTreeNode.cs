using UnityEngine;
using System.Collections;

public class MoveFromLineNodeToTreeNode : MonoBehaviour
{
    public float startTime;
    public float endTime;
    public Vector3 startPosition;
    public Vector3 endPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        if (startTime <= time && time <= endTime)
        {
            this.transform.position = Vector3.Lerp(startPosition, endPosition, 
                (time - startTime) / (endTime - startTime));
        }
    }
}
