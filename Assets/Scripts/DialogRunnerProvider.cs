using DefaultNamespace;
using UnityEngine;
using Yarn.Unity;

public class DialogRunnerProvider : Singletone<DialogRunnerProvider>
{
    [SerializeField] private DialogueRunner _dialogueRunner;

    public DialogueRunner DialogueRunner => _dialogueRunner;
}