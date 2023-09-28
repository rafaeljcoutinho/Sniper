using UnityEngine;
using System.Collections.Generic;

public class SpawnPointController : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    
    private GameObject[] spawnPoints;
    private EnableGameObjects enableGameObjects;
    private List<GameObject> spotsAvailable = new List<GameObject>();
    private int newPosition;
    private GameObject NewPositionGo;

    private void Awake()
    {
        enableGameObjects = GameObject.Find("SceneController").GetComponent<EnableGameObjects>();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach(GameObject point in spawnPoints)
        {
            spotsAvailable.Add(point);
        }
        for(int i=0; i<players.Length; i++)
        {
            var instance = Instantiate(players[i]);
            instance.SetActive(false);
            enableGameObjects.ReviveAfterXSec(instance, 1);
        }
    }


    public void FreePosition(GameObject position)
    {
        spotsAvailable.Add(position);
    }

    public GameObject GetNewPosition()
    {
        newPosition = Random.Range(0, spotsAvailable.Count - 1);
        NewPositionGo = spotsAvailable[newPosition].gameObject;
        spotsAvailable.Remove(spotsAvailable[newPosition].gameObject);
        return NewPositionGo;
    }
}
