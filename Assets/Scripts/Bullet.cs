using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
