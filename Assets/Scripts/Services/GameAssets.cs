using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(fileName = "GameAssets" ,menuName = "GameAssets")]
public class GameAssets : ScriptableObject, IService
{
    public SoundAudioClip[] soundAudioClipArray;
    private Dictionary<SoundManager.Sound, float> audioClipDictionary = new();
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    public float GetLength(SoundManager.Sound sound)
    {
        if (!audioClipDictionary.ContainsKey(sound))
        {
            audioClipDictionary[sound] = soundAudioClipArray.First(clip => clip.sound == sound).audioClip.length;
        }
        return audioClipDictionary[sound];
    }
    public AudioMixer audioMixer;
    public AudioMixerGroup SoundsAudioMixerGroup;
    public AudioMixerGroup MusicAudioMixer;
    public AudioMixerGroup GlobalAudioMixer;
}