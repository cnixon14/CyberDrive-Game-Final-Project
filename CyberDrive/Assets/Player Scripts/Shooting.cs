
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firecool = 0.2f;

    public Camera fpsCam;
    public ParticleSystem Muzzleflash;
    public GameObject Impacteffect;
    public AudioSource gunshot;

    // Update is called once per frame
    void Update()
    {
        if (firecool >= 0)
        {
            firecool -= 1f * Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && firecool <= 0)
        {
            ShootHitscan();
            firecool = 0.2f;
            

        }
    }

    void ShootHitscan()
    {
        Muzzleflash.Play();
        gunshot.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impacts = Instantiate(Impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impacts, 2f);

        }
    }
}
