using System;

public interface IMinigame
{
    public event Action OnGameEnded; // Используем event вместо свойства
    void SetDisabled();
    void StartGame();
}