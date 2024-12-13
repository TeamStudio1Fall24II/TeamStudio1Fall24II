using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] Slider volumeSlider_master;
    [SerializeField][Range(0,1)] float testMasterVol = 1f;
    [SerializeField] Slider volumeSlider_sfx;
    [SerializeField][Range(0, 1)] float testSFXVol = 1f;
    [SerializeField] Slider volumeSlider_music;
    [SerializeField][Range(0, 1)] float testMusicVol = 1f;
    [SerializeField] Slider volumeSlider_dx;
    [SerializeField][Range(0, 1)] float testDXVol = 1f;

    [SerializeField] AudioMixer mixer;

    private void Update()
    {
        SetMasterVolume(testMasterVol);
        SetSFXVolume(testSFXVol);
        SetMusicVolume(testMusicVol);
        SetDXVolume(testDXVol);
    }

    public void SetMasterVolume(float f)
    {
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("MasterVol", (Mathf.Log10(f) * 20f) - .2f);
        if(volumeSlider_master!= null)
        {
            volumeSlider_master.value = f;
        }
    }

    public void SetSFXVolume(float f)
    {
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("SFXVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_sfx != null)
        {
            volumeSlider_sfx.value = f;
        }
    }

    public void SetMusicVolume(float f)
    {
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("MusicVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_music != null)
        {
            volumeSlider_music.value = f;
        }
    }
    public void SetDXVolume(float f)
    {
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("DXVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_dx != null)
        {
            volumeSlider_dx.value = f;
        }
    }

}
