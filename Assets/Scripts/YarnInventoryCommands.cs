using UnityEngine;
using Yarn.Unity;

namespace DefaultNamespace
{
    public class YarnInventoryCommands : MonoBehaviour
    {
        private InventorySystem InventorySystem => Singletone<InventorySystem>.Instance;

        [YarnCommand("AddItem")]
        public void AddItem(string id)
        {
            InventorySystem.AddItem(new UniPair<string, int>(){Key = id, Value = 1});
        }

        [YarnCommand("AddItemWithCount")]
        public void AddItemWithCount(string id, int count)
        {
            InventorySystem.AddItem(new UniPair<string, int>(){Key = id, Value = count});
        }

        [YarnCommand("RemoveItem")]
        public void RemoveItem(string id)
        {
            InventorySystem.RemoveItem((new UniPair<string, int> {Key = id}));
        }
        
        [YarnCommand("RemoveItemWithCount")]
        public void RemoveItemWithCount(string id, int count)
        {
            InventorySystem.RemoveItem((new UniPair<string, int> {Key = id}));
        }
    }
}