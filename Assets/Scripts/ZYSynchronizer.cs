using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ZYSynchronizer : MonoBehaviour
    {
        [SerializeField] private bool _isUpdatable;

        private void Awake()
        {
            if (!_isUpdatable)
            {
                enabled = false;
            }
            Synchronize();
        }

        private void Synchronize()
        {
            
            var pos = transform.position;
            pos.z = transform.position.y;
            transform.position = pos;
        }

        private void LateUpdate()
        {
            Synchronize();
        }
    }
}