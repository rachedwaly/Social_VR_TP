using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public TriggerAnimations animationTrigger;
    public SkinnedMeshRenderer leftControllerRenderer;
    public GameObject pullParticles;
    public GameObject pushParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left Controller"))
        {
            animationTrigger.BattleAnimation();
            pullParticles.gameObject.SetActive(false);
            pushParticles.gameObject.SetActive(true);
            this.gameObject.transform.SetParent(other.transform);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.rotation = other.transform.rotation;
            leftControllerRenderer.enabled = false;
        }
    }
}
