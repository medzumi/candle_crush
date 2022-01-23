using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Widget : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private bool _showOnStart = false;
        
        public string WidgetName;
        private bool _isShowed;

        private Coroutine _coroutine;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Singletone<UIController>.Instance.AddWidget(WidgetName, this);
            if (!_showOnStart)
            {
                gameObject.SetActive(false);
            }

            _isShowed = _showOnStart;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            if (_animator)
            {
                _animator.SetTrigger("Show");
            }
        }

        public IEnumerator ShowAsync(Action<Widget> action)
        {
            gameObject.SetActive(true);
            if (_animator)
            {
                _animator.SetTrigger("Show");
                while (!_isShowed)
                {
                    yield return null;
                }
            }
            action?.Invoke(this);
        }

        public void Hide()
        {
            if (_animator)
            {
                _animator.SetTrigger("Hide");
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public IEnumerator HideAsync(Action<Widget> action)
        {
            if (_animator)
            {
                _animator.SetTrigger("Hide");
                while (_isShowed)
                {
                    yield return null;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
            action?.Invoke(this);
        }

        public void Show(Action<Widget> onShow)
        {
            gameObject.SetActive(true);
            StopCoroutine();
            _coroutine = StartCoroutine(ShowAsync(onShow));
        }

        public void Hide(Action<Widget> onHide)
        {
            StopCoroutine();
            _coroutine = StartCoroutine(HideAsync(onHide));
        }

        void ShowHandler()
        {
            _isShowed = true;
        }
        
        void HideHandler()
        {
            gameObject.SetActive(false);
            _isShowed = false;
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}