using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour // it provides us to access this code from the other scrip easily 
{
    [Serializable] // to see in the inspector panel
    public struct Pool // A struct object can contain private , protected , and public fields; have methods; and be instantiated at runtime, just like a class type.
    {
        public Queue<GameObject> PooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] public Pool[] pools = null; // if there is no pool return empty

    private void Awake()
    {
        // creating queues
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].PooledObjects = new Queue<GameObject>();

            // creating gameobjects and make them unvisible, then attach the objects to their own queue
            // Also you can say AddSizePool() 
            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab);
                obj.SetActive(false);
                pools[i].PooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPoolObject(int objectType)
    {
        if (objectType >= pools.Length) return null;
        // Adding extra size to pool
        if (pools[objectType].PooledObjects.Count == 0)
        {
            AddSizePool(5, objectType);
        }
        // get out the object from pool
        GameObject obj = pools[objectType].PooledObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void SetPoolObject(GameObject pooledObject, int objectType)
    {
        if (objectType >= pools.Length) return; // means return null in void method
        // put the object to pool                                        
        pools[objectType].PooledObjects.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }

    public void AddSizePool(float amount, int objectType)
    {
        for (int i = 0; i < amount;  i++)
        {
            GameObject obj = Instantiate(pools[objectType].objectPrefab);
            obj.SetActive(false);
            pools[objectType].PooledObjects.Enqueue(obj);
        }
    }
}
