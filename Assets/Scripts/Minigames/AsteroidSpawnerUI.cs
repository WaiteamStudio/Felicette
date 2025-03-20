using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAsteroidSpawner : MonoBehaviour
{
    [SerializeField] CanvasAsteroid asteroidPrefab; // Новый астероид с Image
    [SerializeField] Transform asteroidParent; // Родитель (UI-объект)
    [SerializeField] Image spawnZone; // Image вместо BoxCollider2D
    [SerializeField] Image spawnZone2;
    [SerializeField] float spawnTime = 1.5f;

    public void StartSpawning()
    {
        CancelInvoke(nameof(Spawn));
        InvokeRepeating(nameof(Spawn), 0f, spawnTime);
    }

    public void StopSpawning(bool destroyAllSpawned = false)
    {
        if (destroyAllSpawned)
            DestroyAllAsteroids();
        CancelInvoke(nameof(Spawn));
    }

    public void DestroyAllAsteroids()
    {
        foreach (Transform child in asteroidParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Spawn()
    {
        var (spawnPos, targetPos) = CreateMovementDirection();
        CanvasAsteroid asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity, asteroidParent);
        asteroid.Setup(targetPos);
    }

    private Tuple<Vector2, Vector2> CreateMovementDirection()
    {
        bool choice = UnityEngine.Random.Range(0, 2) == 0;
        Image startZone = choice ? spawnZone : spawnZone2;
        Image endZone = choice ? spawnZone2 : spawnZone;
        Vector2 spawnPoint = GetRandomPointOnImage(startZone);
        Vector2 targetPoint = GetRandomPointOnImage(endZone);
        return new Tuple<Vector2, Vector2>(spawnPoint, targetPoint);
    }

    private Vector2 GetRandomPointOnImage(Image image)
    {
        if (image == null) return Vector2.zero;

        RectTransform rectTransform = image.rectTransform;
        Vector2 size = rectTransform.sizeDelta; // Размер Image
        Vector2 randomPoint = new Vector2(
            UnityEngine.Random.Range(-size.x / 2, size.x / 2),
            UnityEngine.Random.Range(-size.y / 2, size.y / 2)
        );

        return (Vector2)rectTransform.position + randomPoint;
    }
}
