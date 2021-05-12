using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileStuff : MonoBehaviour
{

    public float speed = 10f;
    public GameObject projectile;
    public GameObject target;
    public BoxCollider collider;
    public float timer;
    public float damage = 10f;
    public Rigidbody rb;
    Vector3 aim;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        target = GameObject.Find("Player");
        //transform.LookAt(target.transform);
        //Vector3 aim = (target.transform.position - transform.position).normalized;
        //playerPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        //transform.rotation = Quaternion.LookRotation(playerPos);
        //rb.AddForce(transform.forward * speed);
        rb.velocity = (target.transform.position - rb.transform.position).normalized * speed;
        //rb.AddForce(aim * speed);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        //Vector3 aim = (target.transform.position - transform.position).normalized;
        //rb.AddForce(playerPos * speed);
        //rb.velocity = (target.transform.position - rb.transform.position).normalized* speed;
        timer += 1f * Time.deltaTime;

        if (timer >= 10f)
        {
            Destroy(projectile);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Destroy(projectile);
            Debug.Log("Ouch");
        }

        if (collider.gameObject.name == "Environment")
        {
            Destroy(projectile);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player" || collision.collider.gameObject.name == "Player Cylinder")
        {
            //playerStatus.isDamage = true;
        }
        Destroy(projectile);
        Debug.Log(collision.collider.gameObject.name);

    }
}
