using DefaultNamespace;
using UnityEngine;
using Yarn.Unity;

public class MemoryProvider : Singletone<MemoryProvider>
{
    [SerializeField] private VariableStorageBehaviour _variableStorageBehaviour;

    public VariableStorageBehaviour VariableStorageBehaviour => _variableStorageBehaviour;
}