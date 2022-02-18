using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace DefaultNamespace
{
    public class DialogueEventManager : Singletone<DialogueEventManager>
    {
        [SerializeField] private List<UniPair<string, UnityEvent<bool>>> _boolEvents;

        [SerializeField] private List<UniPair<string, UnityEvent<int>>> _intEvents;

        [SerializeField] private List<UniPair<string, UnityEvent<string>>> _stringEvents;

        [YarnCommand("StringCustomCommand")]
        public static void Command(string key, string obj)
        {
            if (Instance)
            {
                Instance.CustomCommand(key, obj);    
            }
        }
        
        [YarnCommand("BoolCustomCommand")]
        public static void Command(string key, bool obj)
        {
            if (Instance)
            {
                Instance.CustomCommand(key, obj);    
            }
        }
        
        [YarnCommand("IntCustomCommand")]
        public static void Command(string key, int obj)
        {
            if (Instance)
            {
                Instance.CustomCommand(key, obj);    
            }
        }

        private void CustomCommand(string key, int obj)
        {
            _intEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value?.Invoke(obj);
        }

        private void CustomCommand(string key, bool obj)
        {
            _boolEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value?.Invoke(obj);
        }
        
        private void CustomCommand(string key, string obj)
        {
            _stringEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value?.Invoke(obj);
        }
    }
}