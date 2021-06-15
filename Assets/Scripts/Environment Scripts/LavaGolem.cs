using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGolem : MonoBehaviour
{
    private Vector3 direction;

    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        direction = (player.transform.position - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
