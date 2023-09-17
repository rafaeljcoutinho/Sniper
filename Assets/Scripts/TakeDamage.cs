using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private float timeToRespawn;
    
    private EnableGameObjects enableGameObjects;


    private void Awake()
    {
        hp = maxHp;
        enableGameObjects = GameObject.Find("SceneController").GetComponent<EnableGameObjects>();
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
