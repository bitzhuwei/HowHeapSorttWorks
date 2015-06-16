using UnityEngine;
using System.Collections;

public class SortingManager : MonoBehaviour
{
    public System.Collections.Generic.List<int> targetList;
    public System.Collections.Generic.List<GameObject> lineNodes;
    public System.Collections.Generic.List<GameObject> treeNodes;
    System.Collections.Generic.Queue<StepInfo> stepQueue;

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

        if (this.lineNodes == null)
        {
            this.lineNodes = new System.Collections.Generic.List<GameObject>();
        }

        if (this.treeNodes == null)
        {
            this.treeNodes = new System.Collections.Generic.List<GameObject>();
        }

        if (this.stepQueue == null)
        {
            this.stepQueue = new System.Collections.Generic.Queue<StepInfo>();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;
        if (queue.Count > 0)
        {
            StepInfo stepInfo = queue.Peek();
            switch (stepInfo.stepType)
            {
                case StepTypes.Unknown:
                    break;
                case StepTypes.Swap:
                    break;
                case StepTypes.BuildSubHeap:
                    BuildSubHeap(stepInfo);
                    break;
                default:
                    break;
            }
            //GameObject target = stepInfo.target;
            //GameObject child = stepInfo.child;
            //stepInfo.passedInterval += Time.deltaTime;
            //if (target == child) // not need to swap.
            //{
            //    Vector3 newPosition = stepInfo.targetPosition;
            //    newPosition.x += Random.Range(-1, 1);
            //    newPosition.y += Random.Range(-1, 1);
            //    newPosition.z += Random.Range(-1, 1);
            //    target.transform.position = newPosition;
            //    if (stepInfo.passedInterval >= stepInfo.interval)
            //    {
            //        queue.Dequeue();
            //    }
            //}
            //else
            //{
            //    target.transform.position = Vector3.Lerp(
            //        stepInfo.targetPosition, stepInfo.childPosition, stepInfo.passedInterval / stepInfo.interval);
            //    child.transform.position = Vector3.Lerp(
            //        stepInfo.childPosition, stepInfo.targetPosition, stepInfo.passedInterval / stepInfo.interval);
            //    if (stepInfo.passedInterval >= stepInfo.interval)
            //    {
            //        int targetIndex = stepInfo.childIndex;

            //        AddStep(targetIndex, StepTypes.BuildSubHeap);
            //    }
            //}
        }
    }

    private void BuildSubHeap(StepInfo stepInfo)
    {
        System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;
        GameObject target = stepInfo.target;
        GameObject child = stepInfo.child;
        stepInfo.passedInterval += Time.deltaTime;
        if (target == child) // not need to swap.
        {
            if (stepInfo.passedInterval < stepInfo.interval)
            {
                Vector3 newPosition = stepInfo.targetPosition;
                const float range = 0.1f;
                newPosition.x += Random.Range(-range, range);
                newPosition.y += Random.Range(-range, range);
                newPosition.z += Random.Range(-range, range);
                target.transform.position = newPosition;
            }
            else
            {
                target.transform.position = stepInfo.targetPosition;
                queue.Dequeue();
            }
        }
        else
        {
            target.transform.position = Vector3.Lerp(
                stepInfo.targetPosition, stepInfo.childPosition, stepInfo.passedInterval / stepInfo.interval);
            child.transform.position = Vector3.Lerp(
                stepInfo.childPosition, stepInfo.targetPosition, stepInfo.passedInterval / stepInfo.interval);
            if (stepInfo.passedInterval >= stepInfo.interval)
            {
                {
                    System.Collections.Generic.List<GameObject> treeNodes = this.treeNodes;
                    GameObject tmp = treeNodes[stepInfo.targetIndex];
                    treeNodes[stepInfo.targetIndex] = treeNodes[stepInfo.childIndex];
                    treeNodes[stepInfo.childIndex] = tmp;
                }

                {
                    System.Collections.Generic.List<GameObject> lineNodes = this.lineNodes;
                    GameObject tmp = lineNodes[stepInfo.targetIndex];
                    lineNodes[stepInfo.targetIndex] = lineNodes[stepInfo.childIndex];
                    lineNodes[stepInfo.childIndex] = tmp;
                }

                queue.Dequeue();

                int targetIndex = stepInfo.childIndex;

                AddStep(targetIndex, StepTypes.BuildSubHeap);
            }
        }
    }

    public void IncreaseStep()
    {
        if (this.stepQueue.Count > 0) { return; }

        this.targetStep++;
       
        int count = targetList.Count;

        if (targetStep <= count / 2 - 1)
        {
            int targetIndex = count / 2 - 1 - targetStep;

            AddStep(targetIndex, StepTypes.BuildSubHeap);

        }

    }

    private void AddStep(int targetIndex, StepTypes stepType)
    {
        System.Collections.Generic.List<int> targetList = this.targetList;
        System.Collections.Generic.List<GameObject> treeNodes = this.treeNodes;

        GameObject target = treeNodes[targetIndex];
        int targetValue = targetList[targetIndex];
        GameObject leftChild = target;
        int leftChildValue = targetValue;
        if (targetIndex * 2 + 1 < treeNodes.Count)
        {
            leftChild = treeNodes[targetIndex * 2 + 1];
            leftChildValue = targetList[targetIndex * 2 + 1];
        }
        GameObject rightChild = target;
        int rightChildValue = targetValue; 
        if (targetIndex * 2 + 2 < treeNodes.Count)
        {
            rightChild = treeNodes[targetIndex * 2 + 2];
            rightChildValue = targetList[targetIndex * 2 + 2];
        }

        GameObject child = target;
        int childIndex = targetIndex;
        int max = targetValue;
        if (targetIndex * 2 + 1 < treeNodes.Count && max < leftChildValue)
        { child = leftChild; childIndex = targetIndex * 2 + 1; max = leftChildValue; }
        if (targetIndex * 2 + 2 < treeNodes.Count && max < rightChildValue)
        { child = rightChild; childIndex = targetIndex * 2 + 2; max = rightChildValue; }

        if (targetIndex * 2 + 1 < treeNodes.Count)
        {
            StepInfo stepInfo = new StepInfo(target, targetIndex, child, childIndex, stepType);
            this.stepQueue.Enqueue(stepInfo);
        }
    }

    //public StepTypes GetStepTypes(int targetStep, int elementCount)
    //{
    //    if (elementCount < 1) { throw new System.ArgumentOutOfRangeException("totalSteps"); }

    //    if (targetStep < 0) { return StepTypes.Unknown; }

    //    if (targetStep <= elementCount / 2 - 1) { return StepTypes.BuildSubHeap; }

    //    targetStep -= elementCount / 2 - 1;
    //    if (targetStep % 2 == 0)
    //    {
    //        return StepTypes.Swap;
    //    }
    //    else
    //    {
    //        return StepTypes.BuildSubHeap;
    //    }
    //}

}
