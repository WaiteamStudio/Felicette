using UnityEngine;
using System.Collections.Generic;

public class WireGameManager : MonoBehaviour
{
    [SerializeField] private List<WirePoint> allStartWirePoints;

    private int totalWires;
    private int connectedWires = 0;
    private bool gameWon = false;

    private void Start()
    {
        totalWires = allStartWirePoints.Count;

        foreach (var wirePoint in allStartWirePoints)
        {
            wirePoint.OnWireConnected += HandleWireConnected;
        }
    }

    private void HandleWireConnected()
    {
        if (gameWon) return;

        connectedWires++;
        CheckForVictory();
    }

    private void CheckForVictory()
    {
        if (connectedWires >= totalWires)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameWon = true;
        Debug.Log("Победа! Все провода соединены правильно!");
    }

    private void OnDestroy()
    {
        foreach (var wirePoint in allStartWirePoints)
        {
            if (wirePoint != null)
            {
                wirePoint.OnWireConnected -= HandleWireConnected;
            }
        }
    }
}
