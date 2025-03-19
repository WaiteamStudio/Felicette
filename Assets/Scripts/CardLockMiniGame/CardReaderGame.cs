using System.Collections;
using UnityEngine;

public class CardReaderGame : MonoBehaviour
{
    [Header("Настройки картридера")]
    public float minTime; 
    public float maxTime;
    public bool requireUpToDown = true;
    public float minDistance; 

    [Header("Визуальная обратная связь")]
    public GameObject greenLight;
    public GameObject redLight;

    [Header("Ивенты")]
    [SerializeField] public GameEvent miniGameEnd;

    private bool isCardInReader = false; 
    private float timeInReader = 0f;  
    private Vector2 entryPosition;

    void Start()
    {
        greenLight.SetActive(false);
        redLight.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Card"))
        {
            isCardInReader = true;
            timeInReader = 0f;
            entryPosition = other.transform.position;
            Debug.Log("Карточка вставлена в картридер!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Card"))
        {
            isCardInReader = false;

            Vector2 exitPosition = other.transform.position;

            bool isTimeValid = timeInReader >= minTime && timeInReader <= maxTime;

            bool isDirectionValid = CheckDirection(entryPosition, exitPosition);

            bool isDistanceValid = CheckDistance(entryPosition, exitPosition);

            if (isTimeValid && isDirectionValid && isDistanceValid)
            {
                StartCoroutine(HandleSuccessfulCardRead());
            }
            else
            {
                ShowRedLight();
            }
        }
    }

    void Update()
    {
        if (isCardInReader)
        {
            timeInReader += Time.deltaTime;
        }
    }

    void ShowGreenLight()
    {
        greenLight.SetActive(true);
        redLight.SetActive(false);
    }

    void ShowRedLight()
    {
        greenLight.SetActive(false);
        redLight.SetActive(true);

        Invoke("TurnOffRedLight", 1f);
    }

    void TurnOffRedLight()
    {
        redLight.SetActive(false);
    }

    bool CheckDirection(Vector2 entryPos, Vector2 exitPos)
    {
        if (requireUpToDown)
        {
            return entryPos.y > exitPos.y;
        }
        else
        {
            return true;
        }
    }

    bool CheckDistance(Vector2 entryPos, Vector2 exitPos)
    {
        float distance = Mathf.Abs(entryPos.y - exitPos.y);

        return distance >= minDistance;
    }

    private IEnumerator HandleSuccessfulCardRead()
    {
        ShowGreenLight();
        yield return new WaitForSeconds(1f);
        miniGameEnd.Raise(this, 0);
    }
}
