using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Resource _resourceToPool;
    [SerializeField] private int _amountToPool;
    private List<Resource> _pooledObjects;
    private List<Resource> PooledObjects => _pooledObjects;

    void Start()
    {
        _pooledObjects = new List<Resource>();
        Resource temp;
        for (int i = 0; i < _amountToPool; i++)
        {
                temp = Instantiate(_resourceToPool);
                temp.gameObject.SetActive(false);
                PooledObjects.Add(temp);
        }
    }
}
