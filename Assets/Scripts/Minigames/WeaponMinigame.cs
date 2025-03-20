using System;
using TMPro;
using UnityEngine;

public class WeaponMinigame : MonoBehaviour, IMinigame
{
    [SerializeField]TextMeshProUGUI ScoreText;
    [SerializeField] CanvasAsteroidSpawner AsteroidSpawner;
    [SerializeField] Transform Crosshair;
    private int score = 0;
    public event Action OnGameEnded;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

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

    private void Start()
    {
        //StartGame();
    }
    public void StartGame()
    {
        score = 0;
        ScoreText.text = "0";
        StartSpawningAsteroids();
    }
    public void MoveCrosshair(Vector3 target)
    {
        Crosshair.transform.position = target;
    }
    public void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
        WinCheck();
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
    }

    private void StartSpawningAsteroids()
    {
        AsteroidSpawner.StartSpawning();
    }

    public void SetDisabled()
    {
        gameObject.SetActive(false);
    }
}
