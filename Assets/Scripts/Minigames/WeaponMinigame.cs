using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMinigame : MonoBehaviour, IMinigame
{
    [SerializeField]TextMeshProUGUI ScoreText;
    [SerializeField] AsteroidSpawner AsteroidSpawner;
    [SerializeField] Transform Crosshair;
    [SerializeField] Canvas _canvas;
    [Header("Events")]
    [SerializeField] public GameEvent astMiniGameEnd;
    private int score = 0;

    public event Action OnGameEnded;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            

        }

    }

    private void Start()
    {
        StartCoroutine(OneSecondDelay());
    }

    IEnumerator OneSecondDelay()
    {
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    public void StopGame()
    {
        Win();
    }
    public Vector3 GetGamePositon()
    {
        return transform.position + new Vector3(0, 0, -1f);
}
    public void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
        WinCheck();
    }

    public void StartGame()
    {
        _canvas.gameObject.SetActive(true);
        score = 0;
        ScoreText.text = "0";
        StartSpawningAsteroids();
    }

    public void SetDisabled()
    {
        //OnGameEnded?.Invoke();
    }
    private void Fire()
    {
        MoveCrosshair(Input.mousePosition);
        //PlaySound
        TryCatchAsteroid(Input.mousePosition);
    }

    private void TryCatchAsteroid(Vector3 mousePosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null)
        {
            Asteroid asteroid = hit.collider.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.DestroyAsteroid();
                AddScore();
            }
        }
    }
    
    private void MoveCrosshair(Vector3 target)
    {
        Crosshair.transform.position = target;
    }
  

    private void WinCheck()
    {
        if(score >= 10)
        {
            Win();
        }
    }

    private void Win()
    {
        AsteroidSpawner.StopSpawning(true);
        //_canvas.enabled = false;
        OnGameEnded?.Invoke();
        StartCoroutine(HandleSuccessfulFinish());
    }

    private IEnumerator HandleSuccessfulFinish()
    {
        yield return new WaitForSeconds(1f);
        astMiniGameEnd.Raise(this, 0);
        GamePlayManager.thirdMissionChecker++;
    }

    private void StartSpawningAsteroids()
    {
        AsteroidSpawner.StartSpawning();
    }
}
