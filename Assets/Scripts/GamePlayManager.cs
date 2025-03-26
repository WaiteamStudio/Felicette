using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TeleportObject teleportObject�FromHall;

    [Header("Events")]
    [SerializeField] public GameEvent enteredReceptionForTheFirstTime;

    private void Update()
    {
        if (teleportObject�FromHall.numbrOfTimesUsed == 1) //�������� � ������ ��� �� ����� � ����
        {
            StartCoroutine(StartCallFromDad());
        }
    }


    private IEnumerator StartCallFromDad()
    {
        yield return new WaitForSeconds(1f);
        enteredReceptionForTheFirstTime.Raise(this, 0);
        teleportObject�FromHall.numbrOfTimesUsed++;
    }

}
