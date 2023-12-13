using GameEvent;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(TaskBook))]
public class Garrison : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    [SerializeField] private float _delayCheckTaskBook = 0.1f;
    [SerializeField] private CollectorEvent _setBuilder;

    private Queue<Collector> _waitingCollectors = new Queue<Collector>();
    private WaitForSeconds _waitTimeSetTarget;
    private TaskBook _taskBook;

    private void Awake()
    {
        _waitTimeSetTarget = new WaitForSeconds(_delayCheckTaskBook);
        _taskBook = GetComponent<TaskBook>();
        StartCoroutine(SetCollectorTarget());
    }

    public void OnRegisteredCollector(Collector collector)
    {        
        _waitingCollectors.Enqueue(collector);
    }

    private void DirectCollectros()
    {
        if (_waitingCollectors.Count > 0 && !_taskBook.IsEmpty)
        {
            Transform target = _taskBook.GetFirst();
            Queue<Transform> targets = new Queue<Transform>();

            targets.Enqueue(target);

            if (target.TryGetComponent<Resource>(out Resource resource))
                targets.Enqueue(transform);

            if (_waitingCollectors.Peek().TrySetTargets(targets))
            {
                Collector collector = _waitingCollectors.Dequeue();
                _taskBook.RemoveFirst();

                if (target.TryGetComponent<ReplicationMark>(out ReplicationMark replicationMark))
                    _setBuilder.Invoke(collector);
            }
        }
    }

    private IEnumerator SetCollectorTarget()
    {
        while (enabled)
        {
            DirectCollectros();

            yield return _waitTimeSetTarget;
        }
    }
}
