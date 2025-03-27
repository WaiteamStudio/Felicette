using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TeleportObject teleportObjectàFromHall;
    public static int thirdMissionChecker = 0;

    [Header("Events")]
    [SerializeField] public GameEvent enteredReceptionForTheFirstTime;
    [SerializeField] public GameEvent endEvent;

    private void Update()
    {
        if (teleportObjectàFromHall.numbrOfTimesUsed == 1) //ïğîâåğêà â ïåğâûé ğàç ëè çàøåë â õîëë
        {
            StartCoroutine(StartCallFromDad());
        }

        if (thirdMissionChecker == 4)
        {
            endEvent.Raise(this, 0);
            thirdMissionChecker++;
        }
    }


    private IEnumerator StartCallFromDad()
    {
        yield return new WaitForSeconds(1f);
        enteredReceptionForTheFirstTime.Raise(this, 0);
        teleportObjectàFromHall.numbrOfTimesUsed++;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MenuTest");
    }

    public void Increment()
    {
        thirdMissionChecker++;
    }
}
