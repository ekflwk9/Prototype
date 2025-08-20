using System.Collections.Generic;
using UnityEngine;


public interface IPoolable
{
    void Pop();
    void Push();
}

public static class PoolManager
{
    public static Dictionary<string, Queue<GameObject>> pool = new();
    public static Dictionary<string, GameObject> parent = new();
    public static GameObject Instance;

    private static void CheckManager()
    {
        if (Instance != null) return;

        var manager = new GameObject("PoolManager");
        MonoBehaviour.DontDestroyOnLoad(manager.gameObject);
        Instance = manager;
    }

    /// <summary>
    /// 특정 키의 값을 오브젝트 포함, 전부 삭제함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void Remove(string _poolObjectName)
    {
        if (Instance == null) return;

        else if (parent.ContainsKey(_poolObjectName))
        {
            var removeParent = parent[_poolObjectName];

            pool.Remove(_poolObjectName);
            parent.Remove(_poolObjectName);
            MonoBehaviour.Destroy(removeParent.gameObject);
        }
    }

    /// <summary>
    /// 해당 오브젝트를 가져온 후 데이터에서 제외함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static GameObject Pop(string _poolObjectName)
    {
        CheckManager();
        GameObject poolObject = null;

        if (pool.ContainsKey(_poolObjectName) && pool[_poolObjectName].Count > 0)
        {
            poolObject = pool[_poolObjectName].Dequeue();
            IPoolable poolComponent = poolObject.GetComponent<IPoolable>();
            if (!poolObject) poolObject = Pop(_poolObjectName);
            poolComponent.Pop();
        }

        else
        {
            GameObject loadObject = ResourcesManager.Instance.GetOnLoadedResource<GameObject>(_poolObjectName);

            if (loadObject != null)
            {
                if (!parent.ContainsKey(_poolObjectName))
                {
                    parent.Add(_poolObjectName, new GameObject($"{_poolObjectName} Parent"));
                    parent[_poolObjectName].transform.SetParent(Instance.transform);
                }

                poolObject = MonoBehaviour.Instantiate(loadObject);
                poolObject.name = _poolObjectName;
                poolObject.transform.SetParent(parent[_poolObjectName].transform);
                IPoolable poolComponent = poolObject.GetComponent<IPoolable>();
                poolComponent.Pop();
            }
        }

        if (poolObject) poolObject.SetActive(true);
        return poolObject;
    }

    /// <summary>
    /// 해당 오브젝트를 풀링에 추가함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_poolingObject"></param>
    public static void Push(GameObject _poolingObject)
    {
        CheckManager();
        if (_poolingObject) _poolingObject.SetActive(false);

        IPoolable poolComponent = _poolingObject.GetComponent<IPoolable>();
        poolComponent.Push();

        if (pool.ContainsKey(_poolingObject.name))
        {
            if (!pool[_poolingObject.name].Contains(_poolingObject))
            {
                pool[_poolingObject.name].Enqueue(_poolingObject);
            }
        }

        else
        {
            var addQueue = new Queue<GameObject>();
            addQueue.Enqueue(_poolingObject);

            pool.Add(_poolingObject.name, addQueue);
        }
    }
}