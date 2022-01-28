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
    
    [SerializeField] private string NPCName;
    [SerializeField] private List<UniPair<string, UnityEvent>> OnCompleteConcreteDialog;

    private DialogueRunner _dialogueRunner => DialogRunnerProvider.Instance.DialogueRunner;

    public void ShowDialog()
    {
        _dialogueRunner.StartDialogue(NPCName);
        _dialogueRunner.onNodeComplete.AddListener(NodeCompleteHandler);
    }

    private void NodeCompleteHandler(string arg0)
    {
        if (string.Equals(NPCName, arg0))
        {
            OnComplete.Invoke();
        }

        OnCompleteConcreteDialog.FirstOrDefault(pair => string.Equals(pair.Key, arg0)).Value?.Invoke();
    }
}
