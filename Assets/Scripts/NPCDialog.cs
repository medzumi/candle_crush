using UnityEngine;
using Yarn.Unity;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] private string NPCName;

    private DialogueRunner _dialogueRunner => DialogRunnerProvider.Instance.DialogueRunner;

    public void ShowDialog()
    {
    }
}
