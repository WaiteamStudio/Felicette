using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMinigame : MonoBehaviour, IMinigame
{
    [SerializeField]TextMeshProUGUI ScoreText;
    [SerializeField] AsteroidSpawner AsteroidSpawner;
    [SerializeField] Transform Crosshair;
    [SerializeField] Canvas _canvas;
    private int score = 0;

    public event Action OnGameEnded;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            

        }

    }

    public void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
        WinCheck();
    }

    public void StartGame()
    {
        _canvas.enabled = true;
        score = 0;
        ScoreText.text = "0";
        StartSpawningAsteroids();
    }

    public void SetDisabled()
    {
        OnGameEnded?.Invoke();
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
        _canvas.enabled = false;
        OnGameEnded?.Invoke();
    }

    private void StartSpawningAsteroids()
    {
        AsteroidSpawner.StartSpawning();
    }

}
