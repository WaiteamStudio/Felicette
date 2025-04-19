using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TeleportObject teleportObjectFromHall;
    [SerializeField] public int thirdMissionChecker = 0; //used to be static

    [Header("Events")]
    [SerializeField] public GameEvent enteredReceptionForTheFirstTime;
    [SerializeField] public GameEvent endEvent;

    private void Update()
    {
        if (teleportObjectFromHall.numbrOfTimesUsed == 1) //�������� � ������ ��� �� ����� � ����
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
        teleportObjectFromHall.numbrOfTimesUsed++;
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
