using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titles : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 30f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
