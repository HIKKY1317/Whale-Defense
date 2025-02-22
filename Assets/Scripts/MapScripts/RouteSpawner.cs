using System.Collections;
using UnityEngine;

public class RouteSpawner : MonoBehaviour
{
    public GameObject routePrefab;
    public float spawnInterval = 0.1f;

    void Start()
    {
        StartCoroutine(SpawnRouteCoroutine());
    }

    private IEnumerator SpawnRouteCoroutine()
    {
        while (true)
        {
            Instantiate(routePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
