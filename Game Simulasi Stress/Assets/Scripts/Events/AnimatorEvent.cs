using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class AnimatorEvent : MonoBehaviour
{
    private Animator animator;

    public List<UnityEvent> OnAnimationTrigger;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationTriggerEvent(int index)
    {
        OnAnimationTrigger[index].Invoke();
    }
}
