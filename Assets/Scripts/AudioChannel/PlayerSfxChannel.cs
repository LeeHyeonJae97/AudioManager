using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxChannel : AudioChannel
{
    private Queue<AudioSource> _sources = new Queue<AudioSource>();

    public override void Play(string name, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        throw new System.NotImplementedException();
    }

    public override AudioSource Play(string name, Transform parent, Vector3 position, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        if (!_clips.TryGetValue(name, out var clip)) return null;

        var source = _sources.Count > 0 ? _sources.Dequeue() : new GameObject().AddComponent<AudioSource>();

        source.name = name;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
        source.transform.SetParent(parent);
        source.transform.localPosition = position;
        source.outputAudioMixerGroup = _mixerGroup;
        source.Play();

        return source;
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    public override void Stop(AudioSource source)
    {
        source.Stop();
        source.transform.SetParent(transform, false);

        _sources.Enqueue(source);
    }
}
