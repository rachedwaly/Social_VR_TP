using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrab : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.8f)
            {
                other.transform.SetParent(transform);
            }
            else
            {
                other.transform.SetParent(null);
            }

            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
