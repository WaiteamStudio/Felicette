using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private CalculatorController calculator;
    [SerializeField] private string digit;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void OnMouseEnter()
    {
        if (hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }

    private void OnMouseExit()
    {
        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void OnMouseDown()
    {
        if (digit == "C")
        {
            calculator.EraseDigit();
        }
        else
        {
            calculator.AddDigit(digit);
        }
    }
}
