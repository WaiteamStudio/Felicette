using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class TelMove : MonoBehaviour
{
    private Vector2 origin;
    private Vector2 difference;
    private Vector2 resetCamera;
    public Camera cam;

    public TelTargets target;
    public float dragSpeed = 0.01f;

    private bool drag = false;


    public float maxFlashSpeed = 0.01f; // Minimum time between flashes when very close
    public float minFlashSpeed = 0.5f; // Maximum time between flashes when far away
    public float maxDistance = 10f; // Distance at which flashing stops
    private Renderer objectRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;


    void Start()
    {
        resetCamera = transform.position;
        Random.InitState(System.DateTime.Now.Millisecond);
        SelectTarget();

        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;


    }

    void SelectTarget()
    {
        // Find all objects of type Target
        TelTargets[] targets = FindObjectsOfType<TelTargets>();

        // Check if there are any targets found
        if (targets.Length == 0)
        {
            Debug.Log("No TelTargets found!");
            return;
        }
        /*
        int randomIndex = Random.Range(0, targets.Length);

        targets[randomIndex].isTarget = true;
        target = targets[randomIndex];*/
        for (int i = 0; i < targets.Length; i++) { 
            if (targets[i].isTarget)
            {
                target = targets[i];
                break;
            }
        }

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetMouseButton(0)) {
            difference = cam.ScreenToWorldPoint(Input.mousePosition)-transform.position;
            if (drag == false)
            {
                drag = true;
                origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else drag = false;
        
        if (drag){
            Vector3 targetPosition = origin - difference;
            transform.position = Vector2.Lerp(transform.position, targetPosition, dragSpeed);
        }
        if (target)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < maxDistance)
            {
                // –ассчитываем скорость мигани€ на основе рассто€ни€
                //float t = Mathf.InverseLerp(maxDistance, 0, distance);
                //float flashSpeed = Mathf.Lerp(minFlashSpeed, maxFlashSpeed, t); // ќбратите внимание на пор€док

                if (flashCoroutine == null)
                {
                    flashCoroutine = StartCoroutine(Flash());
                }
            }
            else
            {
                if (flashCoroutine != null)
                {
                    StopCoroutine(flashCoroutine);
                    flashCoroutine = null;
                    objectRenderer.material.color = originalColor;
                }
            }

            if (Vector2.Distance(transform.position, target.transform.position) < 1) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator Flash()
    {
        Color targetColor = Color.green;
        while (true)
        {
            yield return FlashToColor(originalColor, targetColor, 1);
            yield return FlashToColor(targetColor, originalColor, 2);
        }
    }

    private IEnumerator FlashToColor(Color fromColor, Color toColor, float multiplier)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        float _t = Mathf.InverseLerp(maxDistance, 0, distance);
        float duration = Mathf.Lerp(minFlashSpeed, maxFlashSpeed, _t)*multiplier;

        float lerpTime = 0;
        while (lerpTime < duration)
        {
            lerpTime += Time.deltaTime;
            float t = lerpTime / duration;
            objectRenderer.material.color = Color.Lerp(fromColor, toColor, t);
            yield return null;
        }
        objectRenderer.material.color = toColor;
    }


}
