using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelTargets : MonoBehaviour
{
    public bool isTarget = false;
    void Start()
    {
        /*TargetObject[] targets = FindObjectsOfType<TargetObject>();

        // Check if there are any targets
        if (targets.Length > 0)
        {
            // Randomly select one target
            int randomIndex = Random.Range(0, targets.Length);
            targets[randomIndex].isTarget = true;

            // Optional: Log the selected target for debugging
            Debug.Log("Selected target: " + targets[randomIndex].name);
        }
        else
        {
            Debug.Log("No target objects found!");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
