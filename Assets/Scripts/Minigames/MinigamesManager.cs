using UnityEngine;

public class MinigamesManager : MonoBehaviour, IService
{
    public enum Minigame
    {
        Weapon,
        Code,
        Card,
    }

    [SerializeField] private WeaponMinigame _weaponMinigame;
    private IMinigame _currentMinigame;
    private void Awake()
    {
        
    }
    public void StartMinigame(Minigame minigame)
    {
        if (minigame == Minigame.Weapon)
        {
            _currentMinigame = _weaponMinigame;
            _weaponMinigame.transform.position = Camera.main.transform.position;
            _weaponMinigame.gameObject.SetActive(true);
            _weaponMinigame.OnGameEnded += OnGameEnded;

        }
        ServiceLocator.Current.Get<PlayerMovement>().enabled = false;
        ServiceLocator.Current.Get<PlayerController>().enabled = false;
        _currentMinigame.StartGame();

    }

    private void OnGameEnded()
    {
        if (_currentMinigame != null)
        {
            _currentMinigame.SetDisabled();
            _currentMinigame.OnGameEnded -= OnGameEnded;
            _currentMinigame = null;
            ServiceLocator.Current.Get<PlayerMovement>().enabled = true;
            ServiceLocator.Current.Get<PlayerController>().enabled = true;
        }
    }
}
