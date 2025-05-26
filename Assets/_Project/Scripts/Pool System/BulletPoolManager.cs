using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] BullletPoolItem[] bullletPoolItems;
    private readonly Dictionary<string, Queue<Bullet>> bulletDictionary = new();

    private void Start()
    {
        foreach (var item in bullletPoolItems)
        {
            AddObjectToPooledObject(item.bulletPrefab, item.poolSize);
            item.bulletName = item.bulletPrefab.name;
        }
    }

    public Bullet AddObjectToPooledObject(Bullet bulletPrefab, int size, Transform transformParent = null, string newTag = null)
    {
        if (newTag == null)
            newTag = bulletPrefab.name;

        if (!bulletDictionary.ContainsKey(newTag))
        {
            Queue<Bullet> objPool = new Queue<Bullet>();
            for (int i = 0; i < size; i++)
            {

                var obj = Instantiate(bulletPrefab);
                if (transformParent)
                {
                    obj.transform.SetParent(transformParent);
                    // obj.transform.localPosition = Vector3.zero;
                }
                else
                    obj.transform.SetParent(transform);

                obj.gameObject.SetActive(false);
                objPool.Enqueue(obj);

            }
            bulletDictionary.Add(newTag, objPool);
            return bulletPrefab;
        }

        else return null;

    }

    public Bullet SpawnBulletFromPool(string bulletName)
    {
        var go = bulletDictionary[bulletName].Dequeue();
        bulletDictionary[bulletName].Enqueue(go);
        return go;

    }

    [Serializable]
    public class BullletPoolItem
    {
        public string bulletName;
        public Bullet bulletPrefab;
        public int poolSize = 10;

    }
}
