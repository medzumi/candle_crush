using System;
using System.Collections.Generic;
using DefaultNamespace;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Yarn.Unity;

public class Player : Singletone<Player>
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _skeletonFlipper;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private Light2D _lighting;

    private readonly Dictionary<Type, Dictionary<string, object>> _playerData = new Dictionary<Type, Dictionary<string, object>>();
    private Vector2 _translation = Vector2.zero;

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

        direction = transform.rotation * direction.normalized;
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
        _translation += direction * _speed * Time.deltaTime;
        
    }

    [YarnCommand("IncreasePlayerLighting")]
    public static void IncreaseLight(float increaseValue)
    {
        if (Instance)
        {
            var lighting = Instance._lighting;
            lighting.pointLightInnerRadius += increaseValue;
            lighting.pointLightOuterRadius += increaseValue;
            lighting.intensity += increaseValue;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + (_translation*_speed*Time.fixedDeltaTime ));
        _translation = Vector2.zero;
    }
}
