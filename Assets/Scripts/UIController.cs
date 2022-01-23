using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Canvas))]
    public class UIController : Singletone<UIController>
    {
        [SerializeField] private Canvas _canvas;
        
        private readonly Dictionary<string, Widget> _widgets = new Dictionary<string, Widget>();

        private void Reset()
        {
            _canvas = GetComponent<Canvas>();
        }

        public Widget HideWidget(string key)
        {
            var widget = GetWidget(key);
            widget.Hide();
            return widget;
        }

        public IEnumerator HideWidgetAsync(string key, Action<Widget> onHide = null)
        {
            yield return GetWidget(key).HideAsync(onHide);
        }

        public Widget HideWidget(string key, Action<Widget> onHide)
        {
            var widget = GetWidget(key);
            widget.Hide(onHide);
            return widget;
        }

        public Widget ShowWidget(string key, Action<Widget> onShow)
        {
            var widget = GetWidget(key);
            widget.Show(onShow);
            return widget;
        }
        
        public Widget ShowWidget(string key)
        {
            var widget = GetWidget(key);
            widget.Show();
            return widget;
        }

        public IEnumerator ShowWidgetAsync(string key, Action<Widget> onShow = null)
        {
            yield return GetWidget(key).ShowAsync(onShow);
        }

        public void AddWidget(string widgetName, Widget widget)
        {
            _widgets[widgetName] = widget;
        }

        public Widget GetWidget(string key)
        {
            if (!_widgets.TryGetValue(key, out var widget))
            {
                widget = Resources.Load<Widget>("UI/key");
                _widgets[key] = widget = Instantiate(widget, _canvas.transform);
            }

            return widget;
        }
    }
}