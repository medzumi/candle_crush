using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Yarn;
using Yarn.Unity;

public class NPCDialog : MonoBehaviour
{
    public UnityEvent OnComplete;
    
    [SerializeField] private string StartNode;

    [SerializeField]
    private List<UniPair<string, UnityEvent>> _onCompleteNodes = new List<UniPair<string, UnityEvent>>();
    
    private DialogueRunner _dialogueRunner => DialogRunnerProvider.Instance.DialogueRunner;

    public void ShowDialog()
    {
        _dialogueRunner.StartDialogue(StartNode);
        _dialogueRunner.onNodeComplete.AddListener(NodeCompleteHandler);
    }

    public void HideDialog()
    {
        if (_dialogueRunner.Dialogue != null && _dialogueRunner.Dialogue.IsActive)
        {
            _dialogueRunner.Dialogue.Stop();
            _dialogueRunner.Stop();
            foreach (var VARIABLE in _dialogueRunner.dialogueViews)
            {
                VARIABLE.DismissLine(null);
            }
        }
    }

    private void NodeCompleteHandler(string arg0)
    {
        if (string.Equals(StartNode, arg0))
        {
            OnComplete.Invoke();
        }
        _onCompleteNodes.FirstOrDefault(pair => string.Equals(arg0, pair.Key)).Value?.Invoke();
    }
}
