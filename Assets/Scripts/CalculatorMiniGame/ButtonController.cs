using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private CalculatorController calculator;
    [SerializeField] private string digit;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;

    [Header("Appearance Settings")]
    [SerializeField][Range(0.1f, 1f)] private float hoverAlpha = 0.8f; 
    [SerializeField][Range(1f, 1.5f)] private float clickBrightness = 1.2f; 
    [SerializeField] private float clickEffectDuration = 0.1f; 

    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    private Color hoverColor;
    private bool isMouseOver = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }

        normalColor = spriteRenderer.color;

        hoverColor = new Color(normalColor.r, normalColor.g, normalColor.b, hoverAlpha);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        if (hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
        else
        {
            spriteRenderer.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }

        spriteRenderer.color = normalColor;
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = new Color(
    normalColor.r * clickBrightness,
    normalColor.g * clickBrightness,
    normalColor.b * clickBrightness,
    isMouseOver ? hoverColor.a : normalColor.a);

        Invoke("ResetColorAfterClick", clickEffectDuration);

        if (digit == "C")
        {
            calculator.EraseDigit();
        }
        else
        {
            calculator.AddDigit(digit);
        }
    }

    private void ResetColorAfterClick()
    {
        if (isMouseOver)
        {
            spriteRenderer.color = hoverColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }
    }
}
