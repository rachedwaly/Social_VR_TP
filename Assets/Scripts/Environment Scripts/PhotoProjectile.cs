using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoProjectile : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
