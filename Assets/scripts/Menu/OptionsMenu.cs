using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;

    [Header("Main Volume")]
    [SerializeField]
    private Slider mainVolumeSlider;
    [SerializeField]
    private TextMeshProUGUI mainVolumePercentageText;

    [Header("Music Volume")]
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private TextMeshProUGUI musicVolumePercentageText;

    [Header("SFX Volume")]
    [SerializeField]
    private Slider sfxVolumeSlider;
    [SerializeField]
    private TextMeshProUGUI sfxVolumePercentageText;

    [Header("Camera Sensitivity Settings")]
    [SerializeField]
    private Slider sensSlider;
    [SerializeField]
    private TextMeshProUGUI sensPercentageText;
    public static float sensitivity { get; private set; } = .5f;

    private void Start() {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        audioMixer.GetFloat("SFXVolume", out float sfxVolume);

        if (mainVolumeSlider != null)
            mainVolumeSlider.value = Mathf.Pow(10f, masterVolume / 20f);

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = Mathf.Pow(10f, musicVolume / 20f);

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = Mathf.Pow(10f, sfxVolume / 20f);
        }

        SetSensitivity(sensSlider.value);
    }

    public void SetMasterVolume(float volume) {
        volume = Mathf.Clamp(mainVolumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        mainVolumePercentageText.text = $"{(int)(volume * 100)}%";
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(musicVolumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolumePercentageText.text = $"{(int)(volume * 100)}%";
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(sfxVolumeSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        sfxVolumePercentageText.text = $"{(int)(volume * 100)}%";
    }

    public void SetSensitivity(float sens) {
        sensitivity = sens;
        sensPercentageText.text = $"{(int)(sens * 100)}%";
    }
}
