using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InventoryItemGO))]
public class ItemAnimator : MonoBehaviour
{
    public enum AnimationType
    {
        Collecting,

    }

    private Animator animator;
    private InventoryItemGO InventoryItemGO;

   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        StopAnimation();
        InventoryItemGO = GetComponent<InventoryItemGO>();
        InventoryItemGO.CollectStart.AddListener(OnItemCollectingStart);
    }

    private void OnItemCollectingStart(ItemDetailsSO arg0)
    {
        Debug.Log("Collecting start");
        StartCoroutine(OnItemCollectingStartCoroutine(AnimationType.Collecting));
    }

    private IEnumerator OnItemCollectingStartCoroutine(AnimationType animation)
    {
        PlayAnimation(animation);
        yield return new WaitForSeconds(GetAnimationLength(animation));
        InventoryItemGO.OnCollectEnd();
    }

    public void PlayAnimation(AnimationType animation)
    {
        if (IsAnimmationClipByNameExist(animation.ToString()))
            animator.Play(animation.ToString());
        else
            Debug.LogAssertion("clip" + animation + "not found");
    }
    public void StopAnimation()
    {
        animator.StopPlayback();
    }
    private bool IsAnimmationClipByNameExist(string clipName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if(clip.name == clipName)
            {
                return true;
            }
        }
        return false;
    }
    private float GetAnimationLength(AnimationType animation)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in controller.animationClips)
        {
            Debug.Log("clip" + clip.name + " found ");
            if (clip.name == animation.ToString())
            {
                return clip.length;
            }
        }
        throw new Exception("Animation " + animation + " not found");
    }
}
