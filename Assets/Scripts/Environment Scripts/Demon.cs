using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    private Animator demonAnimator;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        demonAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DemonSlayerSword"))
        {
            if (!isDead)
            {
                isDead = true;

                demonAnimator.SetTrigger("Death");

            }
        }
    }
}
