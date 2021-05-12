using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    public float force = 10f;
    public Collider collider;

    void OnColliderTrigger(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Debug.Log("Hit");
        }

    }
}
