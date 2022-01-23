using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;

    private readonly Dictionary<Type, Dictionary<string, object>> _playerData = new Dictionary<Type, Dictionary<string, object>>();

    public T GetData<T>(string key) where T : class, new()
    {
        var type = typeof(T);
        if (!_playerData.TryGetValue(type, out var data))
        {
            _playerData[type] = data = new Dictionary<string, object>();
        }

        if (!data.TryGetValue(key, out var concreteData))
        {
            concreteData = data[key] = new T();
        }

        return concreteData as T;
    }
    
    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        direction = transform.rotation * direction;
        _characterController.Move(direction.normalized * _speed * Time.deltaTime);
    }
}
