using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected PoolItem[] poolItems;
    protected readonly Dictionary<string, Queue<T>> objectDictionary = new();
    protected bool hidePoolObjectOnInit = true;
    protected virtual void Awake()
    {
        InitializePools();
    }

    protected virtual void InitializePools()
    {
        foreach (var item in poolItems)
        {
            AddObjectsToPool(item.prefab, item.poolSize);
            item.objectName = item.prefab.name;
        }
    }

    public T AddObjectsToPool(T prefab, int size, Transform parent = null, string tag = null)
    {
        if (tag == null)
            tag = prefab.name;

        if (!objectDictionary.ContainsKey(tag))
        {
            var queue = new Queue<T>();
            for (int i = 0; i < size; i++)
            {
                var obj = Instantiate(prefab);
                if (hidePoolObjectOnInit)
                    obj.gameObject.SetActive(false);

                if (parent != null)
                    obj.transform.SetParent(parent);
                else
                    obj.transform.SetParent(transform);

                queue.Enqueue(obj);
            }

            objectDictionary.Add(tag, queue);
            return prefab;
        }

        return null;
    }

    public T SpawnFromPool(string tag)
    {
        if (!objectDictionary.ContainsKey(tag) || objectDictionary[tag].Count == 0)
        {
            Debug.LogWarning($"No pool with tag {tag} exists or it is empty.");
            return null;
        }

        var obj = objectDictionary[tag].Dequeue();
        objectDictionary[tag].Enqueue(obj);
        return obj;
    }

    [Serializable]
    public class PoolItem
    {
        public string objectName;
        public T prefab;
        public int poolSize = 10;
    }
}

