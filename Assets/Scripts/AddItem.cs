using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct UniPair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
}

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

public class ItemView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    public Text Text => _text;
    public Image Image => _image;
}

public class InventoryView : MonoBehaviour
{
    [SerializeField] private ItemView _prefab;
    [SerializeField] private List<UniPair<string, Sprite>> _itemSprites;
    [SerializeField] private Transform _place;

    private readonly Stack<ItemView> _pool = new Stack<ItemView>();
    private readonly Dictionary<string, ItemView> _dictionary = new Dictionary<string, ItemView>();

    private InventorySystem InventorySystem => Singletone<InventorySystem>.Instance;

    private void OnEnable()
    {
        UpdateHandler(InventorySystem.InventoryItems);
    }

    private void OnDisable()
    {
        foreach (var keyValuePair in _dictionary)
        {
            _pool.Push(keyValuePair.Value);
        }   
        _dictionary.Clear();
    }

    private void UpdateHandler(IEnumerable<UniPair<string, int>> items)
    {
        
    }

    private ItemView GetView()
    {
        if (_pool.Count > 0)
        {
            return _pool.Pop();
        }
        else
        {
            return Instantiate(_prefab, _place);
        }
    }

    private void ChangeHandler()
    {
        UpdateHandler(InventorySystem.InventoryItems);
    }
}

public class InventorySystem : Singletone<InventorySystem>
{
    public UnityEvent<UniPair<string, int>> OnItemAdded;
    public UnityEvent<UniPair<string, int>> OnItemRemoved;
    
    public IEnumerable<UniPair<string, int>> InventoryItems => _inventoryItems;
    
    [SerializeField]
    private List<UniPair<string, int>> _inventoryItems;

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
                OnItemAdded?.Invoke(item);
                return;
            }

            index++;
        }
        _inventoryItems.Add(item);
        OnItemAdded?.Invoke(item);
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
                    OnItemRemoved?.Invoke(inventoryItem);
                    return item.Value;
                }
                else
                {
                    var removed = item.Value - uniPair.Value;
                    _inventoryItems.RemoveAt(index);
                    OnItemRemoved?.Invoke(new UniPair<string, int>(){Key = uniPair.Key, Value = removed});
                    return removed;
                }
            }

            index++;
        }

        return 0;
    }
}
