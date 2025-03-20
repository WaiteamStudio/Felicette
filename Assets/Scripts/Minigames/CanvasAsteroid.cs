using System;
using Random = UnityEngine.Random;
using UnityEngine;
using DG.Tweening;
public class CanvasAsteroid : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 targetPosition;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveSpeedDeviation = 0.3f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float rotationSpeedDeviation = 30f;
    [SerializeField] float lifeTime = 20f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(Vector2 to)
    {
        targetPosition = to;
        gameObject.SetActive(true);
        RandomizeSpeedValues();
        Move();
        Rotate();
        Destroy(gameObject, lifeTime);
    }

    private void Move()
    {
        rectTransform.DOAnchorPos(targetPosition, moveSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() => Destroy(gameObject));
    }

    private void Rotate()
    {
        rectTransform.DORotate(Vector3.forward * 360, rotationSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void RandomizeSpeedValues()
    {
        moveSpeed += Random.Range(-moveSpeedDeviation, moveSpeedDeviation);
        rotationSpeed += Random.Range(-rotationSpeedDeviation, rotationSpeedDeviation);
    }
}
