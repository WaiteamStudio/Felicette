using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableForAnim : MonoBehaviour
{
    public GameObject felisette;
    private void OnDisable()
    {
        felisette.GetComponent<Animator>().SetBool("IsInteracting", false);      
    }
}
