using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _skeletonFlipper;
    [SerializeField] private Animator _animator;
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
        var xAbsDirection = Mathf.Abs(direction.x);
        var zAbsDirection = Mathf.Abs(direction.z);
        if (direction.magnitude > Mathf.Epsilon)
        {
            var currentScale = _skeletonFlipper.localScale;
            var multiplier = direction.x >= 0 ? 1 : -1;
            _skeletonFlipper.localScale = new Vector3(Mathf.Abs(currentScale.x) * multiplier, currentScale.y, currentScale.z);
            _animator.SetFloat("Forward", direction.z);
            _animator.SetBool("IsHorizontalMove", Mathf.Abs(direction.z) < Mathf.Epsilon);
        }
        _animator.SetFloat("Speed", direction.magnitude);
        _characterController.Move(direction.normalized * _speed * Time.deltaTime);
    }
}
