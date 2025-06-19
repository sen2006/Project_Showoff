using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider mainVolumeSlider;
    //[SerializeField]
    //private Slider musicVolumeSlider;
    [SerializeField]
    private Slider sensSlider;
    public static float sensitivity { get; private set; } = .5f;

    private void Start() {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        mainVolumeSlider.value = masterVolume;
        //musicVolumeSlider.value = musicVolume;
        SetSensitivity(sensSlider.value);
    }

    public void SetMasterVolume(float volume) {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSensitivity(float sens) {
        sensitivity = sens;
    }
}
