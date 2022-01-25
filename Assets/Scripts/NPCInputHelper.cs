using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class NPCInputHelper : MonoBehaviour
    {
        [SerializeField] private KeyCode _inputKeyCode = KeyCode.E;
        [SerializeField] private Text _text;

        public UnityEvent OnKeyCode;

        private void OnEnable()
        {
            if (_text)
            {
                _text.text = _inputKeyCode.ToString();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_inputKeyCode))
            {
                OnKeyCode?.Invoke();
            }
        }
    }
}