using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

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
        foreach (var uniPair in InventorySystem.InventoryItems)
        {
            UpdateHandler(uniPair);
        }
        InventorySystem.OnItemUpdate.AddListener(UpdateHandler);
    }

    private void OnDisable()
    {
        foreach (var keyValuePair in _dictionary)
        {
            _pool.Push(keyValuePair.Value);
        }   
        _dictionary.Clear();
        InventorySystem.OnItemUpdate.RemoveListener(UpdateHandler);
    }

    private void UpdateHandler(UniPair<string, int> pair)
    {
        if (pair.Value > 0)
        {
            if (!_dictionary.TryGetValue(pair.Key, out var itemView))
            {
                itemView = GetView();
                _dictionary[pair.Key] = itemView;
                itemView.gameObject.SetActive(true);
                itemView.Image.sprite = _itemSprites.FirstOrDefault(spritePair => string.Equals(pair.Key, spritePair.Key)).Value;
            }

            itemView.Text.enabled = pair.Value > 1;
            itemView.Text.text = pair.Value.ToString();
        }
        else
        {
            if (_dictionary.TryGetValue(pair.Key, out var itemView))
            {
                _dictionary.Remove(pair.Key);
                itemView.gameObject.SetActive(false);
                _pool.Push(itemView);
            }
        }
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
}