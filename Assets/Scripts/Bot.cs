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

    private int i = 0;
    private bool target;
    private float timeToChange = 0;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    private void Update()
    {
        if(weapon.AmmoCurrent == 0)
        {
            weapon.Reload();
        }
        if(target == false && timeToChange < Time.time)
        {
            targetSpot = FindNextPosition();
            timeToChange = Time.time + 2f;
        }
        else
        {
            MoveAndShoot();
        }
    }

    private void MoveAndShoot()
    {
        transform.LookAt(targetSpot);
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.WeaponRange, playerLayer))
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
        if (i > spawnPoints.Length - 1)
            i = 0;
        target = true;
        i++;
        return spawnPoints[i-1].transform.position;
    }

}
