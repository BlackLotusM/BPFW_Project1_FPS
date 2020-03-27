using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float FireRate = 20f;

    private float FireTime = 0f;

    public ParticleSystem Fire;

    public Camera fpsCam;
    public AudioSource test;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= FireTime)
        {
            Fire.Play();
            FireTime = Time.time + 1f / FireRate;
            
            shoot();
        }
    }

    public void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            test.Play();
            Debug.Log(hit.transform.name);
            DestroyableObject ob = hit.transform.GetComponent<DestroyableObject>();

            if(ob != null)
            {
                ob.getHit(damage);
            }
        }
    }
}
