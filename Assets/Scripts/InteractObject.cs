using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
    public UnityEvent<Player> OnTriggerWithPlayer;
    public UnityEvent<Player> OnExitTriggerWithPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OnTriggerWithPlayer.Invoke(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OnExitTriggerWithPlayer.Invoke(player);
        }
    }
}
