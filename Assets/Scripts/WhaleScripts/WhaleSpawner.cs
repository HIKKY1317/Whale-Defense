using System.Collections;
using UnityEngine;

public class WhaleSpawner : MonoBehaviour
{
    public GameObject whalePrefab;
    public int initialWhalesPerWave = 1;
    public int waveGrowth = 1;
    public float spawnDelay = 0.05f;
    public float waveInterval = 10f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        while (true)
        {
            int whalesPerWave = initialWhalesPerWave + (currentWave * waveGrowth);
            for (int i = 0; i < whalesPerWave; i++)
            {
                Instantiate(whalePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }
            currentWave++;
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
