using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform shootPoint;

    private GameObject[] spawnPoints;
    private Vector3 targetSpot;
    private Quaternion targetRotation;
    private float speedRotation = 2f;

    private int newTarget;
    private bool target;
    private float timeToChange = 0;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        if (weapon.AmmoCurrent == 0)
        {
            weapon.Reload();
        }
        if(target == false && timeToChange < Time.time)
        {
            targetSpot = FindNextPosition();
            timeToChange = Time.time + Random.Range(2,7);
        }
        else
        {
            LookAtTargetSpot();
            Shoot();
        }
    }

    private void LookAtTargetSpot()
    {
        targetRotation = Quaternion.LookRotation(targetSpot - transform.position);

    }
    private void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.WeaponRange, playerLayer))
        {
            weapon.Shoot(shootPoint);
        }
        else
        {
            target = false;
        }
    }
    private Vector3 FindNextPosition()
    {
        newTarget = Random.Range(0, spawnPoints.Length);
        return spawnPoints[newTarget].transform.position;
    }

}
