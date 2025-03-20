using System;
using UnityEngine;

public interface IMinigame
{
    public event Action OnGameEnded; // Используем event вместо свойства
    void SetDisabled();
    void StartGame();
    public Vector3 GetGamePositon();
}