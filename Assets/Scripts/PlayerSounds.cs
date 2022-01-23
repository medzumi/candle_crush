using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _stepClip;
    
    void step()
    {
        if (_stepClip)
        {
            SoundPlayer.Instance?.Play3D(_stepClip, transform.position);
        }
    }
}
