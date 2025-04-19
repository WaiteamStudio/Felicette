using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WireGameManager : MonoBehaviour
{
    [SerializeField] private List<WirePoint> allStartWirePoints;
    [Header("Events")]
    [SerializeField] public GameEvent wiresMiniGameEnd;

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
        StartCoroutine(HandleSuccessfulFinish());
    }

    private IEnumerator HandleSuccessfulFinish()
    {
        yield return new WaitForSeconds(1f);
        wiresMiniGameEnd.Raise(this, 0);
        //GamePlayManager.thirdMissionChecker++;
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
