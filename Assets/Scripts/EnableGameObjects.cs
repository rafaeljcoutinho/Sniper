using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjects : MonoBehaviour
{

    public void ReviveAfterXSec(GameObject go, float timeToRespawn)
    {
        StartCoroutine(RespawnCoroutine(go, timeToRespawn));
    }


    IEnumerator RespawnCoroutine(GameObject go, float timeToRespawn )
    {
        yield return new WaitForSecondsRealtime(timeToRespawn);
        go.SetActive(true);
        go.GetComponent<TakeDamage>().Revive();
        
        yield return null;
    }
}
