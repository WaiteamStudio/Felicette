using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public UIDocument uiDocument;

    private void Start()
    {
        var root = uiDocument.rootVisualElement;
        var musicSlider = root.Q<Slider>("MusicSlider");
        var soundSlider = root.Q<Slider>("SoundSlider");

        if (audioMixer.GetFloat("MusicVolume", out float musicVolume))
            musicSlider.value = musicVolume;

        if (audioMixer.GetFloat("SoundsVolume", out float soundsVolume))
            soundSlider.value = soundsVolume;

        musicSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat("MusicVolume", evt.newValue);
        });

        soundSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat("SoundsVolume", evt.newValue);
        });
    }
}
