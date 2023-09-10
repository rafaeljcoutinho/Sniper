using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int ammoCapacity;
    [SerializeField] private int damage;
    [SerializeField] private float timeToShoot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TrailRenderer BulletTrail;

    private float lastShootTime;
    private Ray ray;
    private int ammoCurrent;
    private float nextFire;

    private void Awake()
    {
        nextFire = 0;
        ammoCurrent = ammoCapacity;
    }

    public void Shoot()
    {
        if(Time.time > nextFire && ammoCurrent > 0)
        {
            ammoCurrent--;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, firePoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                var takeDamage = hit.collider.gameObject.GetComponent<TakeDamage>();
                if(takeDamage != null)
                {
                    takeDamage.TakeDamageQnt(damage);
                }
            }


            nextFire = Time.time + timeToShoot;
        }

    }

    public void Reload()
    {
        //animation
        ammoCurrent = ammoCapacity;
    }
    
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while(time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;

        Destroy(Trail.gameObject, Trail.time);
    }
}
