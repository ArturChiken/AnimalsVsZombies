using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _myMixer;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _SFXSlider;

    private void Start()
    {
        LoadVolume();
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _myMixer.SetFloat("music", Mathf.Log10(volume)  * 20);
        YG2.saves.musicVolume = volume;
        YG2.SaveProgress();
    }

    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        _myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        YG2.saves.SFXVolume = volume;
        YG2.SaveProgress();
    }

    private void LoadVolume()
    {
        _musicSlider.value = YG2.saves.musicVolume;
        SetMusicVolume();

        _SFXSlider.value = YG2.saves.SFXVolume;
        SetSFXVolume();
    }
}
