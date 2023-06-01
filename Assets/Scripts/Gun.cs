using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] TrailRenderer bulletTrail;

    public void Fire()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();
            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.SetParent(hit.transform);
            Destroy(effect.gameObject, 3f);

            StartCoroutine(TrailRoutine(muzzleFlash.transform.position, hit.point));

            hittable?.Hit(hit, damage);            
        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleFlash.transform.position, Camera.main.transform.forward * maxDistance));            
        }
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trail = Instantiate(bulletTrail, muzzleFlash.transform.position, Quaternion.identity);
        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime/ totalTime;

            yield return null;
        }
        Destroy(trail.gameObject);
    }
}
