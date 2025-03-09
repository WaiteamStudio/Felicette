using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Asteroid Asteroid;
    [SerializeField] Transform AsteroidParent;
    [SerializeField] BoxCollider2D SpawnZone;
    [SerializeField] BoxCollider2D SpawnZone2;
    [SerializeField] float SpawnTime = 1.5f;
    public void StartSpawning()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0f, SpawnTime);
    }
    public void StopSpawning(bool destroyAllSpawned = false)
    {
        if (destroyAllSpawned)
            DestoryAllAsteroids();
        CancelInvoke(nameof(Spawn));
    }
    public void DestoryAllAsteroids()
    {
        foreach (Transform child in AsteroidParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void Spawn()
    {
        Tuple<Vector2, Vector2> tuple = CreateMovementDirectory();
        Asteroid asteroid = Instantiate(Asteroid, tuple.Item1, Quaternion.identity, AsteroidParent);
        asteroid.Setup(tuple.Item2);
    }
    private  Tuple<Vector2,Vector2> CreateMovementDirectory()
    {
        var choice = Random.Range(0, 2) == 0;
        var spawnZone = choice ? SpawnZone : SpawnZone2;
        var targetZone = choice ? SpawnZone2 : SpawnZone;
        Vector2 spawnPoint = GetRandomPointOnBox2DCollider(spawnZone);
        Vector2 targetPoint = GetRandomPointOnBox2DCollider(targetZone);
        return new Tuple<Vector2, Vector2>(spawnPoint, targetPoint);
    }

    private Vector3 GetRandomPointOnBox2DCollider(BoxCollider2D collider)
    {
        Vector2 extents = collider.size * 0.5f;
        Vector2 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y)
        );
        return collider.transform.TransformPoint(point);
    }


    private Vector3 GetRandomPointOnImage(Image image)
    {
        if (image == null) return Vector2.zero;

        RectTransform rectTransform = image.rectTransform;
        Vector2 size = rectTransform.sizeDelta; // Размер Image
        Vector2 randomPoint = new Vector2(
            UnityEngine.Random.Range(-size.x / 2, size.x / 2),
            UnityEngine.Random.Range(-size.y / 2, size.y / 2)
        );

        return randomPoint + new Vector2( image.rectTransform.position.x,image.rectTransform.position.y);
    }
}
