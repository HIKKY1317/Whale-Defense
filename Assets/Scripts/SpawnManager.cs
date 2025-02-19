using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Whale;
    public int spawnCount;
    public Vector3 spawnPosition;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(Whale, spawnPosition, Quaternion.identity);
        }
    }
}
