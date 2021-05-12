using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buzzblade_stuff : MonoBehaviour
{
    public Collider collider;
    public float collCool = 1f;
    public bool canDamage;

    private void Start()
    {
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        if (collCool > 0)
        {
            collCool -= 1f * Time.deltaTime;
        }
        
        transform.Rotate(0, 30, 0);

        if (collCool <= 0)
        {
            canDamage = true;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && canDamage)
        {
            Debug.Log("Hit");
            playerStatus.buzzDamage = true;
            collCool = 1.5f;
            canDamage = false;
        }

    }
}
