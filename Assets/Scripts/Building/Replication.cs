using GameEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.ObjectChangeEventStream;

[RequireComponent(typeof(Store))]
public class Replication : MonoBehaviour
{
    [SerializeField] private List<ResourceQuantity> _requiredResourcesReplication;
    [SerializeField] private ReplicationMark _replicationMarkTemplate;
    [SerializeField] private UnityEvent _createRepliactionMark;
    [SerializeField] private UnityEvent _destroyRepliactionMark;
    [SerializeField] private TransformEvent _setReplicationTask;

    private Store _store;
    private ReplicationMark _replication;
    private Collector _builder;

    private void Awake()
    {
        _store = GetComponent<Store>();
        _store.OnAddedResource += OnAddedResource;
    }

    public void TrySetReplication(Vector3 position)
    {
        Vector3 markPosition = new Vector3(position.x, 0, position.z);

        if (_replication == null)
        {
            _replication = Instantiate(_replicationMarkTemplate, markPosition, Quaternion.identity);
            _createRepliactionMark.Invoke();
        }            
        else
            _replication.transform.position = markPosition;
    }    

    public void OnSetBuilder(Collector collector)
    {
        _builder = collector;        
        collector.OnCompletedBuild += OnCompletedBuild;
    }

    private void OnCompletedBuild(Collector collector)
    {
        Destroy(_replication.gameObject);
        collector.OnCompletedBuild -= OnCompletedBuild;
        _destroyRepliactionMark.Invoke();
        _builder = null;            
        _replication = null;
    }

    private void OnAddedResource()
    {
        if (_replication != null && _builder == null && _store.TrySpendResources(_requiredResourcesReplication))
            _setReplicationTask.Invoke(_replication.transform);
    }

    private void OnDestroy()
    {
        _store.OnAddedResource -= OnAddedResource;
    }
}
