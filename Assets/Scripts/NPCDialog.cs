using System;
using UnityEngine;
using UnityEngine.Events;
using Yarn;
using Yarn.Unity;

public class NPCDialog : MonoBehaviour
{
    public UnityEvent OnComplete;
    
    [SerializeField] private string StartNode;

    private DialogueRunner _dialogueRunner => DialogRunnerProvider.Instance.DialogueRunner;

    public void ShowDialog()
    {
        _dialogueRunner.StartDialogue(StartNode);
        _dialogueRunner.onNodeComplete.AddListener(NodeCompleteHandler);
    }

    private void NodeCompleteHandler(string arg0)
    {
        if (string.Equals(StartNode, arg0))
        {
            OnComplete.Invoke();
        }
    }
}
