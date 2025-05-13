using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableForAnim : MonoBehaviour
{
    public GameObject felisette;
    private void OnDisable()
    {
        felisette.GetComponent<Animator>().SetBool("IsInteracting", false);
        /*Vector3 playerscale = felisette.transform.localScale;
        playerscale.x = 1;
        felisette.transform.localScale = playerscale;*/
        SpriteRenderer spriteRenderer = felisette.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
    }
}
