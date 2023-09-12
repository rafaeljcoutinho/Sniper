using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int ammoCapacity;
    [SerializeField] private int damage;
    [SerializeField] private float timeToShoot;
    [SerializeField] private float distanceScope;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TrailRenderer BulletTrail;

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject ScopeUI;



    private int ammoCurrent;
    private float nextFire;
    private bool isScope;
    private Vector3 screenCenter;

    private void Awake()
    {
        nextFire = 0;
        ammoCurrent = ammoCapacity;
        isScope = false;
    }

    public void Shoot()
    {
        if(Time.time > nextFire && ammoCurrent > 0)
        {
            ammoCurrent--;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, float.MaxValue, Mask))
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

    public void Aim()
    {
        if(isScope == false)
        {
            isScope = true;
            ScopeUI.SetActive(true);
            cam.fieldOfView -= distanceScope;
        }
        else
        {
            isScope = false;
            ScopeUI.SetActive(false);
            cam.fieldOfView += distanceScope;
        }
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
