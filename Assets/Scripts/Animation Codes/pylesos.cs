using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pylesos : MonoBehaviour
{
    private Animator animator;
    //public Object thisobject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("left")) gameObject.SetActive(false);
    }
}
