using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance { get => _instance; private set => _instance = value; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this as T)
        {
            Destroy(this);
        }
        else
        {
            Instance = this as T;
        }
        DontDestroyOnLoad(this);
    }
}

