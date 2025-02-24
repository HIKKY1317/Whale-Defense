using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleSpawner : MonoBehaviour
{
    public GameObject weakWhalePrefab;
    public GameObject whalePrefab;
    public GameObject strongWhalePrefab;
    public Text gameClearText;
    public float spawnDelay = 0.05f;
    public float waveInterval = 10f;
    public string fileName;

    private Queue<Dictionary<GameObject, int>> waves = new Queue<Dictionary<GameObject, int>>();
    private int remainingWhales = 0;

    void Start()
    {
        gameClearText = GameObject.Find("GameClearText").GetComponent<Text>();
        gameClearText.text = "";
        LoadWaveData();
        StartCoroutine(SpawnWaveCoroutine());
    }

    void LoadWaveData()
    {
        fileName = PlayerPrefs.GetString("Level", "Level1");
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("Wave data file not found in Resources!");
            return;
        }

        Dictionary<GameObject, int> currentWaveData = null;
        string[] lines = textAsset.text.Split('\n');

        foreach (string line in lines)
        {
            if (line.StartsWith("Wave:"))
            {
                if (currentWaveData != null)
                    waves.Enqueue(currentWaveData);
                currentWaveData = new Dictionary<GameObject, int>();
            }
            else if (currentWaveData != null)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int count))
                {
                    switch (parts[0].Trim())
                    {
                        case "Whale":
                            currentWaveData[whalePrefab] = count;
                            break;
                        case "WeakWhale":
                            currentWaveData[weakWhalePrefab] = count;
                            break;
                        case "StrongWhale":
                            currentWaveData[strongWhalePrefab] = count;
                            break;
                    }
                }
            }
        }
        if (currentWaveData != null)
            waves.Enqueue(currentWaveData);
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        while (waves.Count > 0)
        {
            Dictionary<GameObject, int> waveData = waves.Dequeue();
            remainingWhales = 0;

            foreach (var kvp in waveData)
                remainingWhales += kvp.Value;

            foreach (var kvp in waveData)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    GameObject whale = Instantiate(kvp.Key, transform.position, Quaternion.identity);
                    whale.AddComponent<WhaleDestroyHandler>().SetSpawner(this);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            while (remainingWhales > 0)
            {
                yield return null;
            }
        }

        PlayerAttributes player = FindFirstObjectByType<PlayerAttributes>();
        if (player.hp > 0) gameClearText.text = "GameClear!";
    }

    public void OnWhaleDestroyed()
    {
        remainingWhales--;
    }

    private class WhaleDestroyHandler : MonoBehaviour
    {
        private WhaleSpawner spawner;

        public void SetSpawner(WhaleSpawner spawner)
        {
            this.spawner = spawner;
        }

        void OnDestroy()
        {
            if (spawner != null)
            {
                spawner.OnWhaleDestroyed();
            }
        }
    }
}
