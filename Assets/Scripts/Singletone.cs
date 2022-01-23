using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Singletone<T> : MonoBehaviour where T : Singletone<T>
    {
        public static T Instance { get; private set; }

        public bool IsDontDestroy;

        protected void Awake()
        {
            Instance = this as T;
            if (IsDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}