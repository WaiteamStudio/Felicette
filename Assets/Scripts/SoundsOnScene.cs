using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnScene : MonoBehaviour
{
    [SerializeField] private Transform pylesosPosition;

    SoundManager _soundManager;
    SoundManager SoundManager

    {
        get
        {
            if (_soundManager == null)
                _soundManager = ServiceLocator.Current.Get<SoundManager>();
            return _soundManager;
        }
    }

    public void PylesosMove()
    {
        SoundManager.PlaySoundInPosition(SoundManager.Sound.PylesosMove, pylesosPosition.position);
    }
}
