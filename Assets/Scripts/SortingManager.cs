using UnityEngine;
using System.Collections;

public class SortingManager : MonoBehaviour
{
    public System.Collections.Generic.List<int> targetList;
    public System.Collections.Generic.List<GameObject> lineNodePositions;
    public System.Collections.Generic.List<GameObject> treeNodePositions;

    int currentStep = -1;
    int targetStep = -1;
    int GetTotalSteps()
    {
        int count = this.targetList.Count;
        int buildHeap = count / 2 - 1;
        int swap = count - 1;
        int buildSubHeap = count - 1;

        return buildHeap + swap + buildSubHeap;
    }

    void Awake()
    {
        if (this.targetList == null)
        {
            this.targetList = new System.Collections.Generic.List<int>();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseStep()
    {
        this.targetStep++;
        StepTypes stepType = GetStepTypes(this.targetStep, this.targetList.Count);
        switch (stepType)
        {
            case StepTypes.Unknown:
                break;
            case StepTypes.Swap:

                break;
            case StepTypes.BuildSubHeap:
                break;
            default:
                break;
        }
    }

    public StepTypes GetStepTypes(int targetStep, int elementCount)
    {
        if (elementCount < 1) { throw new System.ArgumentOutOfRangeException("totalSteps"); }

        if (targetStep < 0) { return StepTypes.Unknown; }

        if (targetStep <= elementCount / 2 - 1) { return StepTypes.BuildSubHeap; }

        targetStep -= elementCount / 2 - 1;
        if (targetStep % 2 == 0)
        {
            return StepTypes.Swap;
        }
        else
        {
            return StepTypes.BuildSubHeap;
        }
    }

}
