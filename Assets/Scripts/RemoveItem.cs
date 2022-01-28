using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Yarn.Unity;

public class RemoveItem : MonoBehaviour
{
    [SerializeField] private List<UniPair<string, int>> _items;

    private InventorySystem InventorySystem => Singletone<InventorySystem>.Instance;
    
    public void Remove()
    {
        foreach (var uniPair in _items)
        {
            InventorySystem.RemoveItem(uniPair);
        }
    }
}
