using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject player;
    public GameObject photoProjectile;
    public GameObject firingPoint;

    private void Start()
    {
        WebCamDisplay.onResultReceived += ShootProjectile;
    }


    private void Update()
    {
        transform.LookAt(player.transform);
    }


    void ShootProjectile()
    {
        GameObject photo = Instantiate(photoProjectile, firingPoint.transform.position, Quaternion.identity);
        int randomForce = Random.Range(40, 70);
        photo.GetComponent<Rigidbody>().AddForce(transform.forward * randomForce, ForceMode.Impulse);
    }
}
