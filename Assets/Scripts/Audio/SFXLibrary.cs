using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public static class SFXLibrary
{                                                                                                                                                                                                                       
    public enum SFXType { Default, Footstep, Wood, Keyboard, Mouse, Monitor, DoorClose, DoorOpen, UI_Click}

    public static Dictionary<SFXType, SFXSO> sfxDictionary = new Dictionary<SFXType, SFXSO>();

    public static void PlaySound(SFXType type, Vector3 location, AudioSource audioSource)
    {
        if (sfxDictionary.Count == 0) 
        {
            var sos = Resources.LoadAll<SFXSO>("Audio");

            foreach (UnityEngine.Object o in sos)
            {
                SFXSO s = (SFXSO)o;
                if (!sfxDictionary.ContainsKey(s.type))
                {
                    sfxDictionary.Add(s.type, s);
                }
                else
                {
                    Debug.Log(s.type + " is already a key in SFX Dictionary");
                }
            }
        }
        Debug.Log(sfxDictionary.Count + " SFX types loaded into Dictionary");

        if (sfxDictionary.ContainsKey(type))
        {
            if (audioSource == null)
            {
                //make one
                AudioSource a = GameObject.Instantiate(audioSource) as AudioSource;
                a.transform.position = location;
                a.clip = sfxDictionary[type].GetClip();
                a.volume = sfxDictionary[type].GetVolume();
                a.pitch = sfxDictionary[type].GetPitch();
                a.Play();
                GameObject.Destroy(a.gameObject, a.clip.length +1f);
            }
            else
            {
                audioSource.clip = sfxDictionary[type].GetClip();
                audioSource.volume = sfxDictionary[type].GetVolume();
                audioSource.pitch = sfxDictionary[type].GetPitch();
                audioSource.Play();
            }
        }
    }
    
}
