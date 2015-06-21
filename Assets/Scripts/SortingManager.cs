using UnityEngine;
using System.Collections;

public class SortingManager : MonoBehaviour
{
    public System.Collections.Generic.List<int> targetList;
    public System.Collections.Generic.List<GameObject> lineNodes;
    public System.Collections.Generic.List<GameObject> treeNodes;
    public bool stop = false;

    System.Collections.Generic.Queue<StepInfo> stepQueue;

    int currentStep = -1;
    int targetStep = -1;

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
        if (this.stop) { return; }

        System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;
        if (queue.Count > 0)
        {
            StepInfo stepInfo = queue.Peek();

            stepInfo.passedInterval += Time.deltaTime;

            MoveALittle(stepInfo);

            if (stepInfo.passedInterval >= stepInfo.interval)
            {
                queue.Dequeue();

                switch (stepInfo.stepType)
                {
                    case StepTypes.Unknown:
                        break;
                    case StepTypes.Swap:
                        if (stepInfo.targetIndex != stepInfo.childIndex)
                        {
                            this.treeNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
                            this.lineNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
                            this.targetList.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);

                            Color sortedColor = new Color(0, 0.5f, 0);
                            stepInfo.treeNodeTarget.GetComponentInChildren<TextMesh>().color = sortedColor;
                            TextMesh[] textMeshes = stepInfo.lineNodeTarget.GetComponentsInChildren<TextMesh>();
                            foreach (var item in textMeshes)
                            {
                                item.color = sortedColor;
                            }
                        }
                        else
                        {
                            throw new System.Exception("this should not happen.");
                        }
                        break;
                    case StepTypes.BuildSubHeap:
                        if (stepInfo.targetIndex != stepInfo.childIndex)
                        {
                            this.treeNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
                            this.lineNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
                            this.targetList.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);

                            int targetIndex = stepInfo.childIndex;

                            AddStep4BuildSubHeap(targetIndex);
                        }
                        else
                        {
                            stepInfo.treeNodeTarget.transform.position = stepInfo.treeNodeTargetPosition;
                            stepInfo.lineNodeTarget.transform.position = stepInfo.lineNodeTargetPosition;
                        }
                        break;
                    default:
                        break;
                }
            }

            //Move(stepInfo);
        }
    }

    private void MoveALittle(StepInfo stepInfo)
    {
        System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;

        GameObject treeNodeTarget = stepInfo.treeNodeTarget;
        GameObject treeNodeChild = stepInfo.treeNodeChild;
        GameObject lineNodeTarget = stepInfo.lineNodeTarget;
        GameObject lineNodeChild = stepInfo.lineNodeChild;

        if (treeNodeTarget == treeNodeChild)
        {
            if (stepInfo.stepType == StepTypes.Swap)
            {
                throw new System.Exception("this should not happen.");
            }
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
            treeNodeTarget.transform.position = Vector3.Lerp(
                stepInfo.treeNodeTargetPosition, stepInfo.treeNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
            treeNodeChild.transform.position = Vector3.Lerp(
                stepInfo.treeNodeChildPosition, stepInfo.treeNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);
            lineNodeTarget.transform.position = Vector3.Lerp(
                stepInfo.lineNodeTargetPosition, stepInfo.lineNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
            lineNodeChild.transform.position = Vector3.Lerp(
                stepInfo.lineNodeChildPosition, stepInfo.lineNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);
        }
    }

    //private void Move(StepInfo stepInfo)
    //{
    //    System.Collections.Generic.Queue<StepInfo> queue = this.stepQueue;

    //    GameObject treeNodeTarget = stepInfo.treeNodeTarget;
    //    GameObject treeNodeChild = stepInfo.treeNodeChild;
    //    GameObject lineNodeTarget = stepInfo.lineNodeTarget;
    //    GameObject lineNodeChild = stepInfo.lineNodeChild;

    //    //stepInfo.passedInterval += Time.deltaTime;

    //    if (treeNodeTarget == treeNodeChild) // not need to swap.
    //    {
    //        if (stepInfo.stepType == StepTypes.Swap)
    //        {
    //            throw new System.Exception("this should not happen.");
    //        }
    //        if (stepInfo.passedInterval < stepInfo.interval)
    //        {
    //            //{
    //            //    Vector3 newPosition = stepInfo.treeNodeTargetPosition;
    //            //    const float range = 0.1f;
    //            //    newPosition.x += Random.Range(-range, range);
    //            //    newPosition.y += Random.Range(-range, range);
    //            //    newPosition.z += Random.Range(-range, range);
    //            //    treeNodeTarget.transform.position = newPosition;
    //            //}
    //            //{
    //            //    Vector3 newPosition = stepInfo.lineNodeTargetPosition;
    //            //    const float range = 0.1f;
    //            //    newPosition.x += Random.Range(-range, range);
    //            //    newPosition.y += Random.Range(-range, range);
    //            //    newPosition.z += Random.Range(-range, range);
    //            //    lineNodeTarget.transform.position = newPosition;
    //            //}
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
    //        //treeNodeTarget.transform.position = Vector3.Lerp(
    //        //    stepInfo.treeNodeTargetPosition, stepInfo.treeNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
    //        //treeNodeChild.transform.position = Vector3.Lerp(
    //        //    stepInfo.treeNodeChildPosition, stepInfo.treeNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);
    //        //lineNodeTarget.transform.position = Vector3.Lerp(
    //        //    stepInfo.lineNodeTargetPosition, stepInfo.lineNodeChildPosition, stepInfo.passedInterval / stepInfo.interval);
    //        //lineNodeChild.transform.position = Vector3.Lerp(
    //        //    stepInfo.lineNodeChildPosition, stepInfo.lineNodeTargetPosition, stepInfo.passedInterval / stepInfo.interval);

    //        if (stepInfo.passedInterval >= stepInfo.interval)
    //        {
    //            this.treeNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
    //            this.lineNodes.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);
    //            this.targetList.SwapElement(stepInfo.targetIndex, stepInfo.childIndex);

    //            queue.Dequeue();

    //            if (stepInfo.stepType == StepTypes.BuildSubHeap)
    //            {
    //                int targetIndex = stepInfo.childIndex;

    //                AddStep4BuildSubHeap(targetIndex);
    //            }
    //            else if(stepInfo.stepType== StepTypes.Swap)
    //            {
    //                treeNodeTarget.GetComponentInChildren<TextMesh>().color = new Color(0, 0.5f, 0);
    //            }
    //        }
    //    }
    //}

    int GetSortedCount()
    {
        int targetStep = this.targetStep;
        int count = targetList.Count;
        int initializationSteps = count / 2 - 1;
        int swapSteps = count - 1;
        int buildSubHeapSteps = count - 1;

        if (targetStep <= initializationSteps) { return 0; }
        if (targetStep > initializationSteps + swapSteps + buildSubHeapSteps) { return count; }

        int sortedCount = (targetStep - initializationSteps) / 2;

        return sortedCount;
    }

    public void IncreaseStep()
    {
        if (this.stepQueue.Count > 0) { return; }

        this.targetStep++;

        int targetStep = this.targetStep;
        System.Collections.Generic.List<int> targetList = this.targetList;
        int count = targetList.Count;

        int initializationSteps = count / 2 - 1;
        int swapSteps = count - 1;
        int buildSubHeapSteps = count - 1;
        if (targetStep > initializationSteps + swapSteps + buildSubHeapSteps) { return; }

        if (targetStep <= initializationSteps) // initialize heap.
        {
            int targetIndex = initializationSteps - targetStep;

            AddStep4BuildSubHeap(targetIndex);

        }
        else
        {
            int sortingStepIndex = targetStep - initializationSteps;
            if (sortingStepIndex % 2 == 1) // swap.
            {
                int tailIndex = count - (sortingStepIndex + 1) / 2;
                StepInfo stepInfo = new StepInfo(0, tailIndex, StepTypes.Swap, this);
                this.stepQueue.Enqueue(stepInfo);
            }
            else // build sub heap.
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
        int targetValue = targetList[targetIndex];

        int leftChildValue = targetValue;
        if (targetIndex * 2 + 1 < unSortedCount)
        {
            leftChildValue = targetList[targetIndex * 2 + 1];
        }

        int rightChildValue = targetValue;
        if (targetIndex * 2 + 2 < unSortedCount)
        {
            rightChildValue = targetList[targetIndex * 2 + 2];
        }

        int childIndex = targetIndex;
        int max = targetValue;
        if (targetIndex * 2 + 1 < unSortedCount && max < leftChildValue)
        {
            childIndex = targetIndex * 2 + 1;
            max = leftChildValue;
        }
        if (targetIndex * 2 + 2 < unSortedCount && max < rightChildValue)
        {
            childIndex = targetIndex * 2 + 2;
            max = rightChildValue;
        }

        if (targetIndex * 2 + 1 < unSortedCount)
        {
            StepInfo stepInfo = new StepInfo(targetIndex, childIndex, StepTypes.BuildSubHeap, this);
            this.stepQueue.Enqueue(stepInfo);
        }
    }

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

    public void Reset()
    {
        this.targetStep = -1;
    }
}
