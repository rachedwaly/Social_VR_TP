using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject player;
    public GameObject photoProjectile;
    public GameObject firingPoint;
    public float randomForce;
    public bool debug;

    private void Start()
    {
        WebCamDisplay.onResultReceived += ShootProjectile;
    }


    private void Update()
    {
        transform.LookAt(player.transform);

        if (debug)
        {
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                ShootProjectile();
            }
        }
    }


    void ShootProjectile()
    {
        GameObject photo = Instantiate(photoProjectile, firingPoint.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 100f, transform.eulerAngles.z));
        photo.GetComponent<Rigidbody>().AddForce(transform.forward * randomForce, ForceMode.Impulse);
    }
}
