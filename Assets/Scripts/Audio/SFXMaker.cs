using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXMaker : MonoBehaviour
{
    [SerializeField] SFXLibrary.SFXType soundEffect = SFXLibrary.SFXType.Default;
    [SerializeField] AudioSource myAudioSource = null;

    [SerializeField] bool playOnAwake = false;

    public virtual void Awake()
    {
        if (myAudioSource == null) {  myAudioSource = GetComponent<AudioSource>();}

        if(playOnAwake)
        {
            MakeSound();
        }
    }

    public void MakeSound()
    {
        try
        {
            SFXLibrary.PlaySound(soundEffect, transform.position, myAudioSource);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
