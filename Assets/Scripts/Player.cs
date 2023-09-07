using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon equipedWeapon;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            equipedWeapon.Shoot();
        }
    }
}
