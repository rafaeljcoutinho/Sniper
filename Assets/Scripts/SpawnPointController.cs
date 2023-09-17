using UnityEngine;
using System.Collections.Generic;

public class SpawnPointController : MonoBehaviour
{
    private GameObject[] spawnPoints;
    int i;
    private Dictionary<GameObject, int> pointIndex = new Dictionary<GameObject, int>();
    [SerializeField] private GameObject[] players;
    private EnableGameObjects enableGameObjects;



    private void Awake()
    {
        enableGameObjects = GameObject.Find("SceneController").GetComponent<EnableGameObjects>();
        i = 0;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach(GameObject point in spawnPoints)
        {
            pointIndex.Add(point, 1);
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
        pointIndex[position] = 1;
    }

    public GameObject GetNewPosition()
    {
        foreach (var pairKeyValue  in pointIndex)
        {
            if(pairKeyValue.Value == 1)
            {
                pointIndex[pairKeyValue.Key] = -1;
                i--;
                return pairKeyValue.Key;
            }
        }
        return null;
    }
}
