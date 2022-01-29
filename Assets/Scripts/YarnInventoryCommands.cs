using UnityEngine;
using Yarn.Unity;

namespace DefaultNamespace
{
    public class YarnInventoryCommands : MonoBehaviour
    {
        private static InventorySystem InventorySystem => Singletone<InventorySystem>.Instance;

        [YarnCommand("AddItem")]
        public static void AddItem(string id)
        {
            InventorySystem.AddItem(new UniPair<string, int>(){Key = id, Value = 1});
        }

        [YarnCommand("AddItemWithCount")]
        public static void AddItemWithCount(string id, int count)
        {
            InventorySystem.AddItem(new UniPair<string, int>(){Key = id, Value = count});
        }

        [YarnCommand("RemoveItem")]
        public static void RemoveItem(string id)
        {
            InventorySystem.RemoveItem((new UniPair<string, int> {Key = id}));
        }
        
        [YarnCommand("RemoveItemWithCount")]
        public static void RemoveItemWithCount(string id, int count)
        {
            InventorySystem.RemoveItem((new UniPair<string, int> {Key = id}));
        }
    }
}