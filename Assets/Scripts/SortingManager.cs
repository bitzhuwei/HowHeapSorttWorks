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
            BuildSubHeap(stepInfo);
            //switch (stepInfo.stepType)
            //{
            //    case StepTypes.Unknown:
            //        break;
            //    case StepTypes.Swap:
            //        BuildSubHeap(stepInfo);
            //        break;
            //    case StepTypes.BuildSubHeap:
            //        BuildSubHeap(stepInfo);
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    //private void Swap(StepInfo stepInfo, StepTypes stepType)
    //{
    //    System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;

    //    GameObject treeNodeTarget = stepInfo.treeNodeTarget;
    //    GameObject treeNodeChild = stepInfo.treeNodeChild;
    //    GameObject lineNodeTarget = stepInfo.lineNodeTarget;
    //    GameObject lineNodeChild = stepInfo.lineNodeChild;

    //    stepInfo.passedInterval += Time.deltaTime;

    //    if (treeNodeTarget == treeNodeChild) // not need to swap.
    //    {
    //        throw new System.Exception("this should not happen.");
    //        if (stepInfo.passedInterval < stepInfo.interval)
    //        {
    //            {
    //                Vector3 newPosition = stepInfo.treeNodeTargetPosition;
    //                const float range = 0.1f;
    //                newPosition.x += Random.Range(-range, range);
    //                newPosition.y += Random.Range(-range, range);
    //                newPosition.z += Random.Range(-range, range);
    //                treeNodeTarget.transform.position = newPosition;
    //            }
    //            {
    //                Vector3 newPosition = stepInfo.lineNodeTargetPosition;
    //                const float range = 0.1f;
    //                newPosition.x += Random.Range(-range, range);
    //                newPosition.y += Random.Range(-range, range);
    //                newPosition.z += Random.Range(-range, range);
    //                lineNodeTarget.transform.position = newPosition;
    //            }
    //        }
    //        else
    //        {
    //            treeNodeTarget.transform.position = stepInfo.treeNodeTargetPosition;
    //            lineNodeTarget.transform.position = stepInfo.lineNodeTargetPosition;
    //            queue.Dequeue();
    //        }
    //    }
    //    else
    //    {
    //        treeNodeTarget.transform.position = Vector3.Lerp(
    //            stepInfo.treeNodeTargetPosition, stepInfo.treeNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
    //        treeNodeChild.transform.position = Vector3.Lerp(
    //            stepInfo.treeNodeChildPosition, stepInfo.treeNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);
    //        lineNodeTarget.transform.position = Vector3.Lerp(
    //            stepInfo.lineNodeTargetPosition, stepInfo.lineNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
    //        lineNodeChild.transform.position = Vector3.Lerp(
    //            stepInfo.lineNodeChildPosition, stepInfo.lineNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);

    //        if (stepInfo.passedInterval >= stepInfo.interval)
    //        {
    //            {
    //                System.Collections.Generic.List<GameObject> treeNodes = this.treeNodes;
    //                GameObject tmp = treeNodes[stepInfo.targetIndex];
    //                treeNodes[stepInfo.targetIndex] = treeNodes[stepInfo.childIndex];
    //                treeNodes[stepInfo.childIndex] = tmp;
    //            }

    //            {
    //                System.Collections.Generic.List<GameObject> lineNodes = this.lineNodes;
    //                GameObject tmp = lineNodes[stepInfo.targetIndex];
    //                lineNodes[stepInfo.targetIndex] = lineNodes[stepInfo.childIndex];
    //                lineNodes[stepInfo.childIndex] = tmp;
    //            }

    //            {
    //                System.Collections.Generic.List<int> targetList = this.targetList;
    //                int tmp = targetList[stepInfo.targetIndex];
    //                targetList[stepInfo.targetIndex] = targetList[stepInfo.childIndex];
    //                targetList[stepInfo.childIndex] = tmp;
    //            }

    //            queue.Dequeue();

    //            if (stepType == StepTypes.BuildSubHeap)
    //            {
    //                int targetIndex = stepInfo.childIndex;

    //                AddStep4BuildSubHeap(targetIndex);
    //            }
    //        }
    //    }
    //}

    private void BuildSubHeap(StepInfo stepInfo)
    {
        System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;

        GameObject treeNodeTarget = stepInfo.treeNodeTarget;
        GameObject treeNodeChild = stepInfo.treeNodeChild;
        GameObject lineNodeTarget = stepInfo.lineNodeTarget;
        GameObject lineNodeChild = stepInfo.lineNodeChild;

        stepInfo.passedInterval += Time.deltaTime;

        if (treeNodeTarget == treeNodeChild) // not need to swap.
        {
            if(stepInfo.stepType== StepTypes.Swap)
            {
                throw new System.Exception("this should not happen.");
            }
            if (stepInfo.passedInterval < stepInfo.interval)
            {
                {
                    Vector3 newPosition = stepInfo.treeNodeTargetPosition;
                    const float range = 0.1f;
                    newPosition.x += Random.Range(-range, range);
                    newPosition.y += Random.Range(-range, range);
                    newPosition.z += Random.Range(-range, range);
                    treeNodeTarget.transform.position = newPosition;
                }
                {
                    Vector3 newPosition = stepInfo.lineNodeTargetPosition;
                    const float range = 0.1f;
                    newPosition.x += Random.Range(-range, range);
                    newPosition.y += Random.Range(-range, range);
                    newPosition.z += Random.Range(-range, range);
                    lineNodeTarget.transform.position = newPosition;
                }
            }
            else
            {
                treeNodeTarget.transform.position = stepInfo.treeNodeTargetPosition;
                lineNodeTarget.transform.position = stepInfo.lineNodeTargetPosition;
                queue.Dequeue();
            }
        }
        else
        {
            treeNodeTarget.transform.position = Vector3.Lerp(
                stepInfo.treeNodeTargetPosition, stepInfo.treeNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
            treeNodeChild.transform.position = Vector3.Lerp(
                stepInfo.treeNodeChildPosition, stepInfo.treeNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);
            lineNodeTarget.transform.position = Vector3.Lerp(
                stepInfo.lineNodeTargetPosition, stepInfo.lineNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
            lineNodeChild.transform.position = Vector3.Lerp(
                stepInfo.lineNodeChildPosition, stepInfo.lineNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);

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

                {
                    System.Collections.Generic.List<int> targetList = this.targetList;
                    int tmp = targetList[stepInfo.targetIndex];
                    targetList[stepInfo.targetIndex] = targetList[stepInfo.childIndex];
                    targetList[stepInfo.childIndex] = tmp;
                }

                queue.Dequeue();

                if (stepInfo.stepType == StepTypes.BuildSubHeap)
                {
                    int targetIndex = stepInfo.childIndex;

                    AddStep4BuildSubHeap(targetIndex);
                }
            }
        }
    }

    int GetSortedCount()
    {
        int targetStep = this.targetStep;
        int count = targetList.Count;
        int initializationSteps = count / 2 - 1;
        int swapSteps = count - 1;
        int buildSubHeapSteps = count - 1;

        if (targetStep <= initializationSteps) { return 0; }
        if (targetStep > initializationSteps + swapSteps + buildSubHeapSteps) { return count; }

        return (targetStep - initializationSteps) / 2;// -1;
    }

    public void IncreaseStep()
    {
        if (this.stepQueue.Count > 0) { return; }

        this.targetStep++;
       
        //int count = targetList.Count;

        //if (targetStep <= count / 2 - 1)
        //{
        //    int targetIndex = count / 2 - 1 - targetStep;

        //    AddStep4BuildSubHeap(targetIndex);

        //}

        int targetStep = this.targetStep;
        System.Collections.Generic.List<int> targetList = this.targetList;
        int count = targetList.Count;

        int initializationSteps = count / 2 - 1;
        int swapSteps = count - 1;
        int buildSubHeapSteps = count - 1;
        if (targetStep > initializationSteps + swapSteps + buildSubHeapSteps) { return; }

        if (targetStep <= initializationSteps)
        {
            int targetIndex = initializationSteps - targetStep;

            AddStep4BuildSubHeap(targetIndex);

        }
        else
        {
            int sortingStepIndex = targetStep - initializationSteps;
            if (sortingStepIndex % 2 == 1) // swap
            {
                int tailIndex = count - (sortingStepIndex + 1) / 2;
                StepInfo stepInfo = new StepInfo(0, tailIndex, StepTypes.Swap, this);
                this.stepQueue.Enqueue(stepInfo);
            }
            else
            {
                AddStep4BuildSubHeap(0);
            }
        }
    }

    private void AddStep4BuildSubHeap(int targetIndex)
    {
        System.Collections.Generic.List<int> targetList = this.targetList;
        System.Collections.Generic.List<GameObject> treeNodes = this.treeNodes;
        int unSortedCount = treeNodes.Count - GetSortedCount();

        //GameObject target = treeNodes[targetIndex];
        int targetValue = targetList[targetIndex];
        //GameObject leftChild = target;
        int leftChildValue = targetValue;
        if (targetIndex * 2 + 1 < unSortedCount)
        {
            //leftChild = treeNodes[targetIndex * 2 + 1];
            leftChildValue = targetList[targetIndex * 2 + 1];
        }
        //GameObject rightChild = target;
        int rightChildValue = targetValue; 
        if (targetIndex * 2 + 2 < unSortedCount)
        {
            //rightChild = treeNodes[targetIndex * 2 + 2];
            rightChildValue = targetList[targetIndex * 2 + 2];
        }

        //GameObject child = target;
        int childIndex = targetIndex;
        int max = targetValue;
        if (targetIndex * 2 + 1 < unSortedCount && max < leftChildValue)
        { 
            //child = leftChild;
            childIndex = targetIndex * 2 + 1;
            max = leftChildValue; 
        }
        if (targetIndex * 2 + 2 < unSortedCount && max < rightChildValue)
        {
            //child = rightChild; 
            childIndex = targetIndex * 2 + 2; 
            max = rightChildValue; 
        }

        if (targetIndex * 2 + 1 < unSortedCount)
        {
            StepInfo stepInfo = new StepInfo(targetIndex, childIndex, StepTypes.BuildSubHeap, this);
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


    public string GetNextStepName()
    {
        if (this.targetList.Count <= 0) { return "undefined."; }

        int targetStep = this.targetStep;

        int count = this.targetList.Count;
        int initializationSteps = count / 2 - 1;
        if (targetStep < initializationSteps) { return "init heap"; }

        int swapSteps = count - 1;
        int buildSubHeapSteps = count - 1;
        if (targetStep >= initializationSteps + swapSteps + buildSubHeapSteps) { return "sorted!"; }

        int sortingStepIndex = targetStep - initializationSteps;

        if (sortingStepIndex % 2 == 0) // swap
        { return "swap"; }
        else
        { return "build sub heap"; }
    }
}
