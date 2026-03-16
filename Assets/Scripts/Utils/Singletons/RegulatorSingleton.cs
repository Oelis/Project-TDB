using UnityEngine;

public class RegulatorSingleton<T> : MonoBehaviour where T : Component 
{
    protected static T instance; 
    
    public static bool HasInstance => instance != null;

    public float InitializationTime { get; private set; }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(T).Name + " Auto-Generated");
                    go.hideFlags = HideFlags.HideAndDontSave;
                    instance = go.AddComponent<T>();    
                }
            }
            return instance;    
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if(!Application.isPlaying) return;
        InitializationTime = Time.time;
        DontDestroyOnLoad(gameObject);

        T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach (T oldInstance in oldInstances)
        {
            if (oldInstance.GetComponent<RegulatorSingleton<T>>().InitializationTime < InitializationTime)
            {
                Destroy(oldInstance.gameObject);
            }
        }
        
        if (instance == null)
        {
            instance = this as T;
        }
       
       
    }
}
