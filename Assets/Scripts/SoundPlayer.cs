using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SoundPlayer : Singletone<SoundPlayer>
{
    [SerializeField] private AudioSource _prefab;
    
    private readonly Stack<AudioSource> _audioSources = new Stack<AudioSource>();

    public void Play2D(AudioClip clip)
    {
        var source = GetSource();
        source.spatialBlend = 0;
        StartCoroutine(PlayRoutine(clip, source));
    }

    public void Play3D(AudioClip clip, Vector3 position)
    {
        var source = GetSource();
        source.spatialBlend = 1;
        source.transform.position = position;
        StartCoroutine(PlayRoutine(clip, source));
    }

    private AudioSource GetSource()
    {
        return _audioSources.Count > 0 ? _audioSources.Pop() : Instantiate(_prefab);
    }

    private IEnumerator PlayRoutine(AudioClip clip, AudioSource audioSource)
    {
        var clipLength = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clipLength);
        audioSource.Stop();
        _audioSources.Push(audioSource);
    }
}
