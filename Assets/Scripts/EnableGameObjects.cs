using System.Collections;
using UnityEngine;

public class EnableGameObjects : MonoBehaviour
{
    private TakeDamage takeDamage;
    private SpawnPointController spawnPoints;
    private void Awake()
    {
        spawnPoints = GameObject.Find("SpawnPoints").GetComponent<SpawnPointController>();
    }
    public void ReviveAfterXSec(GameObject go, float timeToRespawn)
    {
        StartCoroutine(RespawnCoroutine(go, timeToRespawn));
    }


    IEnumerator RespawnCoroutine(GameObject go, float timeToRespawn )
    {
        yield return new WaitForSecondsRealtime(timeToRespawn);

        var aux = spawnPoints.GetNewPosition();
        var takeDamage = go.GetComponent<TakeDamage>();
        if(aux != null)
        {
            go.transform.position = aux.transform.position;
            takeDamage.SetIdxPosition(aux);
        }
            
        go.GetComponent<TakeDamage>().Revive();
        go.SetActive(true);

        yield return null;
    }

   
    public void SpawnTrailHitCoroutine(TrailRenderer Trail, Vector3 HitPoint, int damage, RaycastHit hit)
    {
        StartCoroutine(SpawnTrailHit(Trail, HitPoint, damage, hit));
    }

    private IEnumerator SpawnTrailHit(TrailRenderer Trail, Vector3 HitPoint, int damage, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = HitPoint;
        takeDamage = hit.collider.gameObject.GetComponent<TakeDamage>();
        if (takeDamage != null)
        {
            takeDamage.TakeDamageQnt(damage);
        }

        Destroy(Trail.gameObject, Trail.time);
    }

    public void SpawnTrailCoroutine(TrailRenderer Trail, Vector3 HitPoint)
    {
        StartCoroutine(SpawnTrail(Trail, HitPoint));
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = HitPoint;

        Destroy(Trail.gameObject, Trail.time);
    }
}
