using System;
using UnityEngine;

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
            MakeSound(out float clipLength);
        }
    }

    public void MakeSound(out float clipLength)
    {
        //Debug.Log(gameObject.name + " makes sound " + soundEffect.ToString());
        clipLength = 0;
        try
        {
            SFXSO sound = SFXLibrary.GetSound(soundEffect);

            if (myAudioSource == null)
            {
                //make one
                AudioSource a = (new GameObject()).AddComponent<AudioSource>();
                a.name = soundEffect.ToString();
                a.transform.position = transform.position;
                a.clip = sound.GetClip();
                a.volume = sound.GetVolume();
                a.pitch = sound.GetPitch();
                a.outputAudioMixerGroup = sound.GetAudioGroup();
                a.Play();
                GameObject.Destroy(a.gameObject, a.clip.length + 5f);
            }
            else
            {
                myAudioSource.clip = sound.GetClip();

                myAudioSource.volume = sound.GetVolume();
                myAudioSource.pitch = sound.GetPitch();
                myAudioSource.Play();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
