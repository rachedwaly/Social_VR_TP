using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject player;
    public GameObject photoProjectile;
    public GameObject firingPoint;


    private void Update()
    {
        transform.LookAt(player.transform);

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            ShootProjectile();
        }
    }


    void ShootProjectile()
    {
        GameObject photo = Instantiate(photoProjectile, firingPoint.transform.position, Quaternion.identity);
        int randomForce = Random.Range(40, 70);
        photo.GetComponent<Rigidbody>().AddForce(transform.forward * randomForce, ForceMode.Impulse);
    }
}
