using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int ammoCapacity;
    [SerializeField] private float timeToShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    private int ammoCurrent;

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(firePoint.forward));
    }

    private void Update()
    {
    }
}
