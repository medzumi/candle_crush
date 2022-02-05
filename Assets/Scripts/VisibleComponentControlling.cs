using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class VisibleComponentControlling : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _components;

        private void Awake()
        {
            OnBecameInvisible();
        }

        private void OnBecameVisible()
        {
            foreach (var component in _components)
            {
                component.enabled = true;
            }
        }
        
        private void OnBecameInvisible()
        {
            foreach (var component in _components)
            {
                component.enabled = false;
            }
        }
    }
}