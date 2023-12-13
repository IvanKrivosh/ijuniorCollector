using GameEvent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Store))]
public class Incubator : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    [SerializeField] private float _delayProduce = 0.1f;
    [SerializeField] private List<ResourceQuantity> _requiredResourcesCollector;
    [SerializeField] private Collector _collectorTemplate;
    [SerializeField] private CollectorEvent _createdCollector;
    [SerializeField] private int _countCollectors = 0;    
        
    private List<SpawnPoint> _spawnPoints;
    private Store _store;
    private bool _isTernedOn = false;    

    private void Awake()
    {
        _store = GetComponent<Store>();
        _store.OnAddedResource += OnAddedResource;
        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();

        for (int i = 0; i < _countCollectors; i++)
            CreateCollector(_spawnPoints[i].transform.position);        

        TurnOn();
    }

    public void TurnOn()
    {
        _isTernedOn = true;        
    }

    public void TurnOff()
    {
        _isTernedOn = false;        
    }

    private void OnAddedResource()
    {
        if (_isTernedOn &&_store.TrySpendResources(_requiredResourcesCollector))
            CreateCollector();
    }

    private void CreateCollector(Vector3? position = null)
    {
        Vector3 collectorPosition = position.HasValue ? position.Value : _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
        Collector collector = Instantiate(_collectorTemplate, collectorPosition, Quaternion.identity);

        _createdCollector.Invoke(collector);
    }

    private void OnDestroy()
    {
        _store.OnAddedResource -= OnAddedResource;
    }
}
