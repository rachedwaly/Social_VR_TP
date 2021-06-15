using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    private Animator demonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        demonAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DemonSlayerSword"))
        {
            demonAnimator.SetTrigger("Death");
        }
    }
}
