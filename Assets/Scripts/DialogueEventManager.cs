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

        [YarnCommand("CustomCommand")]
        public static void Command(string key, object obj)
        {
            if (Instance)
            {
                Instance.CustomCommand(key, obj);    
            }
        }

        private void CustomCommand(string key, object obj)
        {
            if (obj is int intObj)
            {
                _intEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value.Invoke(intObj);   
            }else if (obj is string stringObj)
            {
                _stringEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value.Invoke(stringObj);
            }else if (obj is bool boolObj)
            {
                _boolEvents.FirstOrDefault(pair => string.Equals(pair.Key, key)).Value.Invoke(boolObj);
            }
        }
    }
}