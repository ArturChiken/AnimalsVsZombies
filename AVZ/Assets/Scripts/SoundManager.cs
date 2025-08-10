using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public delegate void OnVolumeChanged(float volume, List<VolumeType> affected);
    public static OnVolumeChanged onVolumeChanged;
    
    [SerializeField] Slider volumeSlider;
    [SerializeField] List<VolumeType> controllables;
    [SerializeField] string prefName;
    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(prefName, 1);
        onVolumeChanged?.Invoke(volumeSlider.value, controllables);
    }
    public void OnVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat(prefName, volume);
        onVolumeChanged?.Invoke(volume, controllables);
    }

}

public enum VolumeType { SFX, Music }
