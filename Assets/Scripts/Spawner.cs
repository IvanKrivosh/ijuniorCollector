using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _spawnTime = 5.0f;
    [Range(1.0f, 10.0f)]
    [SerializeField] private int _radiusSpawn = 5;

    private WaitForSeconds _spawnDelay;    

    private void Awake()
    {        
        _spawnDelay = new WaitForSeconds(_spawnTime);
        StartCoroutine(SpawnResource());
    }

    private IEnumerator SpawnResource()
    {
        while(enabled)
        {
            int resourceIndex = Random.Range(0, _resources.Count);            

            Instantiate(_resources[resourceIndex], Randomizer.GetHorisontalPosition(_radiusSpawn, _radiusSpawn), Quaternion.identity);
            yield return _spawnDelay;
        }
    }
}
