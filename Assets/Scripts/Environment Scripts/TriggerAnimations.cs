using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimations : MonoBehaviour
{
    [Header("Animators")]
    public Animator demonAnimator;
    public Animator golemAnimator;

    private bool isAnimatorTriggered = false;

    public void BattleAnimation()
    {
        StartCoroutine(AnimationSequence());

    }

    IEnumerator AnimationSequence()
    {
        if (!isAnimatorTriggered)
        {
            isAnimatorTriggered = true;

            yield return null;

            golemAnimator.SetTrigger("Punching");

            yield return new WaitForSeconds(1.5f);

            demonAnimator.SetTrigger("Taking Punch");

            yield return new WaitForSeconds(1.5f);

            demonAnimator.SetTrigger("Punching");

            yield return new WaitForSeconds(1.5f);

            golemAnimator.SetTrigger("Death");
        }
    }
}
