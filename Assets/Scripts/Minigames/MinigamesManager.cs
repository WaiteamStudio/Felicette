using UnityEngine;

public class MinigamesManager : MonoBehaviour, IService
{
    private Vector3 previousCameraPos ;
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
            //_weaponMinigame.transform.position = Camera.main.transform.position;
            //_weaponMinigame.gameObject.SetActive(true);
        }
        _currentMinigame.OnGameEnded += OnGameEnded;
        ServiceLocator.Current.Get<PlayerMovement>().enabled = false;
        ServiceLocator.Current.Get<PlayerController>().enabled = false;
        _currentMinigame.StartGame();
        previousCameraPos = Camera.main.transform.position;
        Camera.main.transform.position = _currentMinigame.GetGamePositon();

    }
    [ContextMenu("Win")]
    public void Win()
    {
        _currentMinigame.StopGame();
    }
    private void OnGameEnded()
    {
        if (_currentMinigame != null)
        {
            //_currentMinigame.SetDisabled();
            _currentMinigame.OnGameEnded -= OnGameEnded;
            _currentMinigame = null;
            Camera.main.transform.position = previousCameraPos;
            ServiceLocator.Current.Get<PlayerMovement>().enabled = true;
            ServiceLocator.Current.Get<PlayerController>().enabled = true;
        }
    }
}
