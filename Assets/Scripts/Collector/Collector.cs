using GameEvent;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Builder))]
public class Collector : MonoBehaviour
{
    [SerializeField] private TransformEvent _setTarget;
    [SerializeField] private ResourceEvent _reachedResource;

    private Queue<Transform> _targets;
    private Builder _builder;

    public delegate void CompletedBuildHandler(Collector builder);
    public event CompletedBuildHandler OnCompletedBuild;

    public Transform Target { get; private set; }
    public bool IsBusy => Target != null;

    private void Awake()
    {
        _builder = GetComponent<Builder>();
    }

    public bool TrySetTargets(Queue<Transform> targets)
    {
        if (!IsBusy)
        {
            _targets = targets;
            Target = _targets.Dequeue();
            _setTarget.Invoke(Target);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnReachedTarget(Transform target)
    {
        Target = null;

        if (target.TryGetComponent<Resource>(out Resource resource))
        {
            _reachedResource.Invoke(resource);
        }
        else if (target.TryGetComponent<Garrison>(out Garrison garrison))
        {
            garrison.OnRegisteredCollector(this);
        }
        else if (target.TryGetComponent<ReplicationMark>(out ReplicationMark replicationMark))
        {         
            _builder.BuildNewStore(target.position);            
            OnCompletedBuild.Invoke(this);
        }       

        if (_targets.Count > 0)
        {
            Target = _targets.Dequeue();
            _setTarget.Invoke(Target);
        }
    }

}
