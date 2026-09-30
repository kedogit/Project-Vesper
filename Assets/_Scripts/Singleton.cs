using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T m_instance;
    public static T Instance => m_instance;

    [SerializeField] private bool m_dontDestroyOnLoad;

    protected virtual void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }

        m_instance = this as T;

        if(m_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {

    }
}
