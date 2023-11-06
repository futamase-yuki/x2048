using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            
            _instance = (T) FindObjectOfType(typeof(T));

            if (_instance != null)
            {
                return _instance;
            }
            
            var g = new GameObject { name = typeof(T).Name };
            var t = g.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            _instance = g.AddComponent<T>();

            return _instance;
        }
    }

    private void Awake()
    {
        DeleteDuplicateInstance();
        DontDestroyOnLoad(_instance.gameObject);
    }

    private void DeleteDuplicateInstance()
    {
        _instance = _instance ? _instance : (T)this;
        // 自身でないInstanceが生成されていた場合は重複フラグを立てる
        var isDuplication = Instance != this;
        if (isDuplication) Destroy(gameObject);
    }
}