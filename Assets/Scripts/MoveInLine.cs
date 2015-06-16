using UnityEngine;
using System.Collections;

/// <summary>
/// Move game object from place to another in specified time.
/// </summary>
public class MoveInLine : MonoBehaviour
{
    public float startTime;
    public float endTime;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private bool done = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (done) { return; }

        float time = Time.time;
        if (time < startTime) { return; }

        if (endTime < time)
        {
            this.transform.position = endPosition;
            done = true;
            return;
        }

        this.transform.position = Vector3.Lerp(startPosition, endPosition,
            (time - startTime) / (endTime - startTime));
    }
}
