using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int ammoCapacity;
    [SerializeField] private int damage;
    [SerializeField] private float timeToShoot;
    [SerializeField] private float distanceScope;
    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TrailRenderer BulletTrail;

    [SerializeField] private float zoomMultiplier;

    private int ammoCurrent;
    private float nextFire;
    private bool isScope;
    
    private RaycastHit hit;
    private bool Hitted;
    private TakeDamage takeDamage;

    public float WeaponRange => weaponRange;

    private void Awake()
    {
        nextFire = 0;
        ammoCurrent = ammoCapacity;
        isScope = false;
        
    }
    private void Update()
    {

    }

    public void AdjustCameraZoom(Camera cam, float camInit, Slider slider)
    {
        if (isScope == true)
        {
            cam.fieldOfView = distanceScope * slider.value * zoomMultiplier;
        }
        else
        {
            slider.value = slider.maxValue;
            cam.fieldOfView = camInit;
        }
    }

    public void EnableScope()
    {
        isScope = true;
    }

    public void DisableScope()
    {
        isScope = false;
    }

    public void Shoot(Transform shootPoint)
    {
        if(Time.time > nextFire && ammoCurrent > 0)  
        {
            ammoCurrent--;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange, Mask))
            {
                Hitted = true;
                TrailRenderer trail = Instantiate(BulletTrail, shootPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point));
            }
            else
            {
                Hitted = false;
                TrailRenderer trail = Instantiate(BulletTrail, shootPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, (shootPoint.position + shootPoint.forward * weaponRange)));
            }


            nextFire = Time.time + timeToShoot;
        }

    }

    public void Reload()
    {
        //animation
        ammoCurrent = ammoCapacity;
    }
    
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while(time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit;
        if (Hitted)
        {
            takeDamage = hit.collider.gameObject.GetComponent<TakeDamage>();
            if (takeDamage != null)
            {
                takeDamage.TakeDamageQnt(damage);
            }
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
