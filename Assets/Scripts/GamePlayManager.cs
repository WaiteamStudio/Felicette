using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TeleportObject teleportObjectàFromHall;

    [Header("Events")]
    [SerializeField] public GameEvent enteredReceptionForTheFirstTime;

    private void Update()
    {
        if (teleportObjectàFromHall.numbrOfTimesUsed == 1) //ïğîâåğêà â ïåğâûé ğàç ëè çàøåë â õîëë
        {
            StartCoroutine(StartCallFromDad());
        }
    }


    private IEnumerator StartCallFromDad()
    {
        yield return new WaitForSeconds(1f);
        enteredReceptionForTheFirstTime.Raise(this, 0);
        teleportObjectàFromHall.numbrOfTimesUsed++;
    }

}
