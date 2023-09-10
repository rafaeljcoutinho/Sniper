using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private float timeToRespawn;
    [SerializeField] private EnableGameObjects enableGameObjects;


    private void Awake()
    {
        hp = maxHp;
    }
    public void TakeDamageQnt(int qnt)
    {
        hp -= qnt;
        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enableGameObjects.ReviveAfterXSec(gameObject, timeToRespawn);
        gameObject.SetActive(false);
    }

        
    public void Revive()
    {
        hp = maxHp;
    }
}
