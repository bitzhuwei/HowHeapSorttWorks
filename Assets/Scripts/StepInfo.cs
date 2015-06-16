using UnityEngine;
using System.Collections;

public class StepInfo
{
    public StepTypes stepType;

    internal float passedInterval;
    public float interval = 0.5f;

    public Vector3 targetPosition;
    public Vector3 childPosition;
    public int targetIndex;
    public int childIndex;
    public GameObject target;
    public GameObject child;

    public StepInfo(GameObject target, int targetIndex, GameObject child, int childIndex, StepTypes stepTypes)
    {
        this.target = target;
        this.targetIndex = targetIndex;
        this.child = child;
        this.childIndex = childIndex;
        this.stepType = stepTypes;

        this.targetPosition = target.transform.position;
        this.childPosition = child.transform.position;
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}<->{2} in {3}", stepType, target, child, interval);
        //return base.ToString();
    }
}