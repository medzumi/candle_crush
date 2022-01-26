using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
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
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        direction = transform.rotation * direction;
        var xAbsDirection = Mathf.Abs(direction.x);
        var zAbsDirection = Mathf.Abs(direction.y);
        if (direction.magnitude > Mathf.Epsilon)
        {
            var currentScale = _skeletonFlipper.localScale;
            var multiplier = direction.x >= 0 ? 1 : -1;
            _skeletonFlipper.localScale = new Vector3(Mathf.Abs(currentScale.x) * multiplier, currentScale.y, currentScale.z);
            _animator.SetFloat("Forward", direction.y);
            _animator.SetBool("IsHorizontalMove", Mathf.Abs(direction.y) < Mathf.Epsilon);
        }
        _animator.SetFloat("Speed", direction.magnitude);
        _rigidbody.MovePosition(_rigidbody.position + (direction*_speed*Time.deltaTime ));
    }
}
