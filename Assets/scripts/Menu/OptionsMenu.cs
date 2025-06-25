using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider mainVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider sensSlider;
    public static float sensitivity { get; private set; } = .5f;

    private void Start() {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        audioMixer.GetFloat("MusicVolume", out float musicVolume);

        if (mainVolumeSlider != null)
            mainVolumeSlider.value = Mathf.Pow(10f, masterVolume / 20f);

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = Mathf.Pow(10f, musicVolume / 20f);

        SetSensitivity(sensSlider.value);
    }

    public void SetMasterVolume(float volume) {
        volume = Mathf.Clamp(mainVolumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(musicVolumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSensitivity(float sens) {
        sensitivity = sens;
    }
}
