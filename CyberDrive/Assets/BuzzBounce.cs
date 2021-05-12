using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzBounce : MonoBehaviour
{
    public Rigidbody rb;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "bounce")
        {

            Vector3 pushDirection = other.transform.position - transform.position;

            pushDirection -= pushDirection.normalized;

            rb.AddForce(pushDirection * rb.velocity.magnitude * rb.velocity.magnitude);

        }
    }
}
