using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SoundManager : IService
{
    public enum Sound {
        PlayerAttack,
        PlayerJump,
        PlayerMove,
        PlayerGetDamaged,
        EnemyAttack,
        EnemyDie,
        EnemyMove,
        EnemyGetDamaged,
        ButtonOver,
        ButtonClick,
        ItemCollected,
        ItemIsHolding
    }
    public enum AudioGroup
    {
        sounds,
        music,
        global
    }

    private Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>()
    {
         
    };
    private Dictionary<Sound, AudioSource> soundSourcesDictionary = new();
    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;
    private GameAssets gameAssets;
    private GameAssets GameAssets
    {
        get {
            if(gameAssets == null)
                gameAssets = ServiceLocator.Current.Get<GameAssets>();
            return gameAssets;
        }
    }

    public void PlaySoundInPosition(Sound sound, Vector3 position) {
        if (CanPlaySound(sound)) {
            if (!soundSourcesDictionary.ContainsKey(sound))
            {
                CreateSourceForSound(sound);
            }
            PlaySoundInPosition(sound, soundSourcesDictionary[sound], position);
            //Object.Destroy(soundGameObject, audioSource.clip.length);
        }
        else
            SetAudioSourcePosition(oneShotAudioSource, position);
    }

    private void CreateSourceForSound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        soundSourcesDictionary[sound] = audioSource;
    }

    private void PlaySoundInPosition(Sound sound, AudioSource audioSource, Vector3 position)
    {
        SetAudioSourcePosition(audioSource, position);
        audioSource.Play();
    }

    private static void SetAudioSourcePosition(AudioSource audioSource, Vector3 position)
    {
        audioSource.transform.position = position;
    }

    public void StopSound(Sound sound)
    {
        if (soundSourcesDictionary[sound] != null)
        {
            soundSourcesDictionary[sound].Stop();
            soundTimerDictionary[sound] = 0f;
        }
    }
    //public void PlaySound(Sound sound, AudioGroup audioGroup = AudioGroup.sounds) {
    //    if (CanPlaySound(sound)) {
    //        if (oneShotGameObject == null) {
    //            oneShotGameObject = new GameObject("One Shot Sound");
    //            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
    //            oneShotAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(audioGroup);
    //        }
    //        AudioClip clip = GetAudioClip(sound);
    //        if (clip != null)
    //        {
    //            oneShotAudioSource.PlayOneShot(clip);
    //        }
    //    }
    //}

    private bool CanPlaySound(Sound sound) {
        switch (sound) {
        default:
            return true;
        case Sound.PlayerMove:
        case Sound.EnemyMove:
        case Sound.ItemIsHolding:
            if (!soundTimerDictionary.ContainsKey(sound))
            {
                soundTimerDictionary[sound] = Time.time;
                return true;
            }
            float lastTimePlayed = soundTimerDictionary[sound];
            float soundTimerMax = gameAssets.GetLength(sound);
            if (lastTimePlayed + soundTimerMax < Time.time) {
                soundTimerDictionary[sound] = Time.time;
                return true;
            } else {
                return false;
            }
        }
    }
    private AudioMixerGroup GetAudioMixerGroup(AudioGroup audioGroup)
    {
        switch (audioGroup)
        {
            case AudioGroup.global:
                return GameAssets.GlobalAudioMixer;
            case AudioGroup.music:
                return GameAssets.MusicAudioMixer;
            case AudioGroup.sounds:
                return GameAssets.SoundsAudioMixerGroup;
            default:
                return gameAssets.GlobalAudioMixer;
        }
    }
    private AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound " + sound + " not found!");
        return null;
    }
}
