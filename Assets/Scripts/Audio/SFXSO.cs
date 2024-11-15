using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SFX", menuName = "Audio Data")]
[Serializable]
public class SFXSO : ScriptableObject
{
    [SerializeField] internal SFXLibrary.SFXType type;
    [SerializeField] List<AudioClip> clips;
    [SerializeField] AudioMixerGroup outputMixerGroup;

    [Header("Volume")]
    [SerializeField][Range(0f, 1f)] float baseVolume = 1.0f;
    [SerializeField][Range(0f, 1f)] float volumeModPercent = .1f;

    [Header("Pitch")]
    [SerializeField][Range(0f, 3f)] float basePitch = 1.0f;
    [SerializeField][Range(0f, 1f)] float pitchModPercent = .1f;

    public AudioMixerGroup GetAudioGroup()
    {
        return outputMixerGroup;
    }

    public float GetVolume()
    {
        return Mathf.Clamp(UnityEngine.Random.Range(-volumeModPercent, volumeModPercent) + baseVolume, 0f, 1f);
    }

    public float GetPitch()
    {
        return Mathf.Clamp(UnityEngine.Random.Range(-pitchModPercent, pitchModPercent) + basePitch, 0f, 3f);
    }

    public AudioClip GetClip()
    {
        if(clips.Count >0)
        {
            return clips[UnityEngine.Random.Range(0, clips.Count - 1)];
        }
        return null;
    }
}
