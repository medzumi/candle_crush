using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    [SerializeField]
    private List<SpriteShadow> _spriteShadows = new List<SpriteShadow>();

    [SerializeField] private AnimationCurve _shadowCurvey;

    [SerializeField] private Transform _lightPoint;

    private void Reset()
    {
        _spriteShadows.AddRange(FindObjectsOfType<SpriteShadow>());
    }

    private void Update()
    {
        foreach (var spriteShadow in _spriteShadows)
        {
            var transformShadow = spriteShadow.transform;
            var delta = _lightPoint.position - transformShadow.position;
            var distance = (delta).magnitude;
            transformShadow.localScale = new Vector3(1, _shadowCurvey.Evaluate(distance), 1);
            var rot = Quaternion.LookRotation(delta);
            var rotEuler = transformShadow.rotation.eulerAngles;
            rotEuler.y = rot.eulerAngles.y;
            transformShadow.rotation = Quaternion.Euler(rotEuler);
        }
    }
}
