using System.Collections;
using TMPro;
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private int maxCodeLength = 6;
    [Header("Events")]
    [SerializeField] public GameEvent miniGameEnd;

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
            StartCoroutine(HandleRightCode());
        }
    }

    private IEnumerator HandleRightCode()
    {
        yield return new WaitForSeconds(1f);
        EndGame();
    }

    private void EndGame()
    {
        miniGameEnd.Raise(this, 0);
    }
}
