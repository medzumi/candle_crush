using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Yarn.Unity;

public class AddItem : MonoBehaviour
{
    [SerializeField] private List<UniPair<string, int>> _addItems;

    private InventorySystem InventorySystem => Singletone<InventorySystem>.Instance;

    public void Add()
    {
        foreach (var uniPair in _addItems)
        {
            InventorySystem.AddItem(uniPair);
        }
    }
}