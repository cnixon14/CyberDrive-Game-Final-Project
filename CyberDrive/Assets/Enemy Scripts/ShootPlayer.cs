using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace enemy
{
    public class ShootPlayer : MonoBehaviour
    {
        public GameObject target;
        public GameObject projectile;
        public GameObject ownSelf;
        public RaycastHit hit;
        public float distance;

        public float speed;
        public float fireTimer;
        public float targetDistance;
        float timeHolder;
        public Vector3 currPosition;
        public float cool = 0.25f;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Player");
            timeHolder = fireTimer;
            

        }

        // Update is called once per frame
        void Update()
        {
            distance = Vector3.Distance(target.transform.position, transform.position);

            cool -= 1f * Time.deltaTime;

            currPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (cool <= 0f)
            {
                Physics.Raycast(ownSelf.transform.position, target.transform.position - transform.position, out hit);
                cool = 0.25f;
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.name);
                }
            }

           
            //Physics.Raycast(transform.position, target.transform.position, out hit);

            Debug.DrawRay(ownSelf.transform.position, target.transform.position - transform.position);

            if (hit.transform != null)
            {
                if ((hit.transform.name == "PlayerCylinder") && (timeHolder <= 0f) && (distance <= targetDistance))
                {
                    fireProjectile(currPosition);
                    timeHolder = fireTimer;
                }
            }

            timeHolder -= 1f * Time.deltaTime;
        }

        public void fireProjectile(Vector3 temp)
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}

