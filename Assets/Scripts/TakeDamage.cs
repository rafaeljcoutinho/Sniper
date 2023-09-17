using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private float timeToRespawn;
    
    private EnableGameObjects enableGameObjects;
    private SpawnPointController spawnPointController;
    private GameObject idxPosition;


    private void Awake()
    {
        hp = maxHp;
        enableGameObjects = GameObject.Find("SceneController").GetComponent<EnableGameObjects>();
        spawnPointController = GameObject.Find("SpawnPoints").GetComponent<SpawnPointController>();
    }
    public void TakeDamageQnt(int qnt)
    {
        hp -= qnt;
        if(hp <= 0)
        {
            Die();
        }
    }

    public void SetIdxPosition(GameObject SpawnPoint)
    {
        idxPosition = SpawnPoint;
    }

    private void Die()
    {
        spawnPointController.FreePosition(idxPosition);
        enableGameObjects.ReviveAfterXSec(gameObject, timeToRespawn);
        gameObject.SetActive(false);
    }

        
    public void Revive()
    {
        hp = maxHp;
    }
}
