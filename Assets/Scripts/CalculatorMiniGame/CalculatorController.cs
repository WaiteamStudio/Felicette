using TMPro;
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private int maxCodeLength = 6;

    private string currentInput = "";
    private string secretCode = "384467";

    void Start()
    {
        UpdateDisplay();
    }

    public void AddDigit(string digit)
    {
        if (currentInput.Length < maxCodeLength) 
        {
            currentInput += digit;
            UpdateDisplay();
            CheckForSecretCode();
        }
        else
        {
            Debug.Log("Достигнута максимальная длина кода!");
        }
    }

    public void EraseDigit()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = currentInput;
        }
    }

    private void CheckForSecretCode()
    {
        if (currentInput == secretCode)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Игра завершена! Вы ввели правильную комбинацию.");
    }
}
