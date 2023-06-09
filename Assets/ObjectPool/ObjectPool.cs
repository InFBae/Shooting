using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{


public class ObjectPool : MonoBehaviour
{
    [SerializeField] Poolable poolablePrefab;

    [SerializeField] int poolSize;
    [SerializeField] int poolCapacity;

    private Stack<Poolable> objectPool = new Stack<Poolable>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
    }

    public Poolable Get()
    {
        if(objectPool.Count > 0)
        {
            Poolable poolable = objectPool.Pop();
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            poolable.Pool = this;

            return poolable;
        }
        else
        {
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.Pool = this;
            return poolable;
        }
        
    }

    public void Release(Poolable poolable)
    {
        if(objectPool.Count < poolCapacity)
        {
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
        else
        {
            Destroy(poolable.gameObject);
        }
        
    }
}
}
