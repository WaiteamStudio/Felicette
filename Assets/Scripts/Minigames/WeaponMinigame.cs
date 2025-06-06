﻿using System;
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
    //private readonly Rect allowedArea = new Rect(285.34f, 207.51f, 1339.65f, 631.38f);

    public event Action OnGameEnded;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            Debug.Log("Mouse: " + Input.mousePosition);


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
        if (!IsClickInsideScreen(Input.mousePosition))
            return;

        SoundManager.PlaySoundInPosition(SoundManager.Sound.Blaster, transform.position);
        MoveCrosshair(Input.mousePosition);
        //PlaySound
        TryCatchAsteroid(Input.mousePosition);
    }

    private bool IsClickInsideScreen(Vector3 screenPos)
    {
        // Левая верхняя точка — (197, 141), ширина 1526, высота 798
        //return allowedArea.Contains(screenPos);
        float normalizedX = screenPos.x / Screen.width;
        float normalizedY = screenPos.y / Screen.height;

        Rect normalizedArea = new Rect(0.15f, 0.19f, 0.70f, 0.58f);

        return normalizedArea.Contains(new Vector2(normalizedX, normalizedY));
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
                if (asteroid.destroyed == false)
                {
                    asteroid.destroyed = true;
                    asteroid.DestroyAsteroid();
                    AddScore();
                }
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
        //GamePlayManager.thirdMissionChecker++;
    }

    private void StartSpawningAsteroids()
    {
        AsteroidSpawner.StartSpawning();
    }
}
