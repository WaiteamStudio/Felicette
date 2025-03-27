using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TeleportObject teleportObject�FromHall;
    public static int thirdMissionChecker = 0;

    [Header("Events")]
    [SerializeField] public GameEvent enteredReceptionForTheFirstTime;
    [SerializeField] public GameEvent endEvent;

    private void Update()
    {
        if (teleportObject�FromHall.numbrOfTimesUsed == 1) //�������� � ������ ��� �� ����� � ����
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
        teleportObject�FromHall.numbrOfTimesUsed++;
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
