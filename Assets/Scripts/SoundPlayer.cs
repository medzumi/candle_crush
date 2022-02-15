using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SoundPlayer : Singletone<SoundPlayer>
{
    [SerializeField] private AudioSource _prefab;
    [SerializeField] private List<UniPair<string, AudioClip>> _audioClips;
    [SerializeField] private List<UniPair<string, AudioClip[]>> _groupClips;
    
    private readonly Stack<AudioSource> _audioSources = new Stack<AudioSource>();

    public void Play2D(AudioClip clip)
    {
        var source = GetSource();
        source.spatialBlend = 0;
        StartCoroutine(PlayRoutine(clip, source));
    }

    public void Play2D(string key)
    {
        var clip = _audioClips.FirstOrDefault(pair => string.Equals(pair.Key, key));
        if (clip.Value)
        {
            Play2D(clip.Value);
        }
    }

    public void PlayRandomFromGroup2D(string key)
    {
        var clips = _groupClips.FirstOrDefault(pair => string.Equals(pair.Key, key));
        if (clips.Value != null && clips.Value.Length > 0)
        {
            var clip = clips.Value[Random.Range(0, clips.Value.Length)];
            Play2D(clip);
        }
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
