using System.Collections.Generic;
using UnityEngine;


public static class SFXLibrary
{                                     
    //only add new sound tags to the end of the list to avoid index swoopling around
    public enum SFXType { 
        Default, Footstep, Wood, Keyboard, KeyImpact, Mouse, Monitor, DoorClose, DoorOpen, UI_Click, 
        Thunder, ElevatorOpen, ElevatorClose, Empty, Reloading, Reloaded, EnemyDamage, EnemyDeath, EnemyDetect, PlayerDamage, PlayerDeath }

    public static Dictionary<SFXType, SFXSO> sfxDictionary = new Dictionary<SFXType, SFXSO>();

    public static SFXSO GetSound(SFXType type)
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
                    //Debug.Log(s.type + " is already a key in SFX Dictionary");
                }
            }
            Debug.Log(sfxDictionary.Count + " SFX types loaded into Dictionary");
        }


        if (sfxDictionary.ContainsKey(type))
        {
            return sfxDictionary[type];
        }
        else return sfxDictionary[SFXType.Default];
    }
    
}
