using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimintoaquarium : MonoBehaviour
{

    private Animator animator;
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;
    public BoxCollider2D helmetcollider;
    private bool stopcheck = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopcheck)
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EndSwim"))
        {

            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
            helmetcollider.enabled = true;
                stopcheck = true;

            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            }
    }
}
