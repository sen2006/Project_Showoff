using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider mainVolumeSlider;
    public static float sensitivity {  get; private set; }

    private void Start()
    {
        audioMixer.GetFloat("MasterVolume", out float volume);
        mainVolumeSlider.value = volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSensitivity(float sens) {
        sensitivity = sens;
    }
}
