using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private int _countCollectors;
    [SerializeField] private Collector _collector;
    [SerializeField] private List<Resource> _resources = new List<Resource>();

    private List<Collector> _collectors;
    private List<SpawnPoint> _spawnPoints;
    private Queue<Resource> _queueResources = new Queue<Resource>();
    private Dictionary<string, int> _store = new Dictionary<string, int>();

    private void Awake()
    {
        _collectors = new List<Collector>();

        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();

        for (int i = 0; i < _countCollectors; i++)
        {
            Collector collector = Instantiate(_collector, _spawnPoints[i].transform.position, Quaternion.identity);
            collector.TrySetOwner(this);
            _collectors.Add(collector);
        }

        _resources.ForEach(resource => _store.Add(resource.GetType().Name, 0));
    }

    public void OnFoundResource(Resource resource)
    {
        Collector freeCollector = _collectors.Find(collector => !collector.IsBusy);

        if (freeCollector == null || !freeCollector.TrySetTarget(resource.transform))
            _queueResources.Enqueue(resource);
    }
    
    public void TryAddResource(Collector colector, Resource resource)
    {
        _store[resource.GetType().Name]++;
        Destroy(resource.gameObject);

        if (_queueResources.Count > 0)        
            colector.TrySetTarget(_queueResources.Dequeue().transform);        
    }
}
