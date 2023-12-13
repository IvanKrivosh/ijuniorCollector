using System.Collections.Generic;
using UnityEngine;

public class TaskBook : MonoBehaviour
{
    private List<Transform> _queueTarget = new List<Transform>();
    private Transform _taskFirstInLine;

    public bool IsEmpty => _queueTarget.Count == 0;

    public void OnRegisteredTask(Transform tranformTarget)
    {
        AddTask(tranformTarget, tranformTarget.TryGetComponent<ReplicationMark>(out ReplicationMark replicationMark));
    }    

    public Transform GetFirst()
    {
        if (IsEmpty) return null;

        _taskFirstInLine = _queueTarget[0];
        return _taskFirstInLine;
    }

    public void RemoveFirst()
    {
        if (_taskFirstInLine != null)
        {
            _queueTarget.Remove(_taskFirstInLine);
            _taskFirstInLine = null;
        }
    }

    private void AddTask(Transform transform, bool isFirstInLine = false)
    {
        if (!isFirstInLine)
            _queueTarget.Add(transform);
        else
            _queueTarget.Insert(0, transform);
    }

}
