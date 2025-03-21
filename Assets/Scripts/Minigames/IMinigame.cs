using System;
using UnityEngine;

public interface IMinigame
{
    public event Action OnGameEnded; // Используем event вместо свойства
    void StartGame();
    void StopGame();
    public Vector3 GetGamePositon();
}