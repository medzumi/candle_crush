using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ZYSynchronizer : MonoBehaviour
    {
        [SerializeField] private bool _isUpdatable;
        [SerializeField] private Vector3 _offset = Vector3.zero;

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
            pos += _offset;
            transform.position = pos;
        }

        private void LateUpdate()
        {
            Synchronize();
        }
    }
}