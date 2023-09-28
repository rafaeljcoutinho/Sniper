using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int ammoCapacity;
    [SerializeField] private int damage;
    [SerializeField] private float timeToShoot;
    [SerializeField] private float timeToReload;
    [SerializeField] private float distanceScope;
    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float zoomMultiplier;

    private int ammoCurrent;
    private float nextFire;
    private bool isScope;

    private RaycastHit hit;
    private EnableGameObjects enableGameObjects;

    public float WeaponRange => weaponRange;
    public int AmmoCurrent => ammoCurrent;

    private void Awake()
    {
        enableGameObjects = GameObject.Find("SceneController").GetComponent<EnableGameObjects>();
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
                TrailRenderer trail = Instantiate(BulletTrail, shootPoint.position, Quaternion.identity);
                enableGameObjects.SpawnTrailHitCoroutine(trail, hit.point, damage, hit);
            }
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, shootPoint.position, Quaternion.identity);
                enableGameObjects.SpawnTrailCoroutine(trail, shootPoint.position + shootPoint.forward * weaponRange);
            }


            nextFire = Time.time + timeToShoot;
        }

    }

    public void Reload()
    {
        //animation
        StartCoroutine(ReloadCoroutine(timeToReload));
    }

    IEnumerator ReloadCoroutine(float timeToReload)
    {
        yield return new WaitForSeconds(timeToReload);
        ammoCurrent = ammoCapacity;
        yield return null;
    }

}
