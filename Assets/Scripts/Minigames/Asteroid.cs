using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    Vector2 target;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveSpeedDeviation = 0.3f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float rotationSpeedDeviation = 0.3f;
    [SerializeField] float lifeTime = 20f;
    [SerializeField] int asteroidType = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite[] sprites; 

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        asteroidType = 1;//UnityEngine.Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[asteroidType];
        animator.SetInteger("type", asteroidType);
    }
    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("destroyed"))Destroy(this.gameObject);
    }
    public void DestroyAsteroid()
    {
        animator.SetTrigger("destroy");
    }

    public void Setup(Vector2 to)
    {
        target = to;
        gameObject.SetActive(true);
        RandomizeSpeedValues();
        Destroy(gameObject, lifeTime);

    }
    
    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
    }

    private void RandomizeSpeedValues()
    {
        moveSpeed += UnityEngine.Random.Range(-moveSpeedDeviation, moveSpeedDeviation);
        rotationSpeed += UnityEngine.Random.Range(-rotationSpeedDeviation, rotationSpeedDeviation);
    }
}
