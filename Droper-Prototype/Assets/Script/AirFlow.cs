using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlow : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;  
    public float force = 10f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.AddForce(direction.normalized * force, ForceMode.Acceleration);
        }
    }
}
