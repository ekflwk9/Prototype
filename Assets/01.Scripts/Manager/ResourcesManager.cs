using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourcesManager
{
    Dictionary<string, UnityEngine.Object> LoadedDictionary = new Dictionary<string, UnityEngine.Object>();

    static ResourcesManager instance;

    public static ResourcesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResourcesManager();
            }
            return instance;
        }
    }

    public async Task LoadResource<T>(string _key) where T : UnityEngine.Object
    {
        if (false == LoadedDictionary.ContainsKey(_key))
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(_key);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                LoadedDictionary.Add(_key, handle.Result);
            }
            else
            {
                LogHelper.LogWarrning($"어드레서블에 없거나 이유모를 실패 {_key}");
            }
        }
        else
        {
            LogHelper.LogWarrning($"이미 로드한 에셋 {_key}");
        }
    }

    public void ReleaseAllResources()
    {
        foreach (KeyValuePair<string, UnityEngine.Object> keyValuePair in LoadedDictionary)
        {
            Addressables.Release(keyValuePair.Value);
        }
        LoadedDictionary.Clear();
    }

    public void ReleaseResource(string _key)
    {
        if (LoadedDictionary.ContainsKey(_key) == true)
        {
            Addressables.Release(LoadedDictionary[_key]);
            LoadedDictionary.Remove(_key);
        }
    }

    public T GetOnLoadedResource<T>(string _key) where T : UnityEngine.Object
    {
        if (LoadedDictionary.ContainsKey(_key))
        {
            return (T)LoadedDictionary[_key];
        }
        LogHelper.LogWarrning(_key);
        LogHelper.LogWarrning("없는 에셋을 가져오려고했음");
        return null;
    }
}
