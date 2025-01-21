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
        var volumeSlider = root.Q<Slider>("SoundSlider");

        audioMixer.GetFloat("MainVolume", out float currentVolume);
        volumeSlider.value = currentVolume;

        volumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat("MainVolume", evt.newValue);
        });
    }
}
