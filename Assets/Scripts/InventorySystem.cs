using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

[DefaultExecutionOrder(-1)]
public class InventorySystem : Singletone<InventorySystem>
{
    public UnityEvent<UniPair<string, int>> OnItemUpdate;
    
    public IEnumerable<UniPair<string, int>> InventoryItems => _inventoryItems;
    
    [SerializeField]
    private List<UniPair<string, int>> _inventoryItems;

    private VariableStorageBehaviour VariableStorageBehaviour =>
        Singletone<MemoryProvider>.Instance.VariableStorageBehaviour;

    public void AddItem(UniPair<string, int> item)
    {
        var index = 0;
        foreach (var uniPair in _inventoryItems)
        {
            if (string.Equals(uniPair.Key, item.Key))
            {
                var inventoryItem = uniPair;
                inventoryItem.Value += item.Value;
                _inventoryItems[index] = inventoryItem;
                OnItemUpdate?.Invoke(inventoryItem);
                SetYarnItem(inventoryItem);
                return;
            }

            index++;
        }
        _inventoryItems.Add(item);
        SetYarnItem(item);
        OnItemUpdate?.Invoke(item);
    }

    public int RemoveItem(UniPair<string, int> item)
    {
        var index = 0;
        foreach (var uniPair in _inventoryItems)
        {
            if (string.Equals(uniPair.Key, item.Key))
            {
                if (uniPair.Value > item.Value)
                {
                    var inventoryItem = uniPair;
                    inventoryItem.Value -= item.Value;
                    _inventoryItems[index] = inventoryItem;
                    OnItemUpdate?.Invoke(inventoryItem);
                    SetYarnItem(inventoryItem);
                    return item.Value;
                }
                else
                {
                    var removed = item.Value - uniPair.Value;
                    _inventoryItems.RemoveAt(index);
                    var updateItemValue = new UniPair<string, int>(){Key = uniPair.Key, Value = 0};
                    OnItemUpdate?.Invoke(updateItemValue);
                    SetYarnItem(updateItemValue);
                    return removed;
                }
            }

            index++;
        }

        return 0;
    }

    private void SetYarnItem(UniPair<string,int> pair)
    {
        VariableStorageBehaviour.SetValue('$' +pair.Key, pair.Value);
    }
}