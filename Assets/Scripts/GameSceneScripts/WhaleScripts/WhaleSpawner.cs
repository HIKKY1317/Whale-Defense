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
    public Text waveText;
    public float spawnDelay = 0.05f;
    public float waveInterval = 10f;
    public string fileName;

    private Queue<Dictionary<GameObject, int>> waves = new Queue<Dictionary<GameObject, int>>();
    private int remainingWhales = 0;
    private int currentWave = 0;
    private bool isFirstWave = true;

    void Start()
    {
        gameClearText = GameObject.Find("GameClearText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        gameClearText.text = "";
        waveText.text = "";
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
            currentWave++;
            Dictionary<GameObject, int> waveData = waves.Dequeue();

            foreach (var kvp in waveData)
                remainingWhales += kvp.Value;

            waveText.text = "Wave: " + currentWave;
            yield return new WaitForSeconds(1f);

            if (isFirstWave)
            {
                waveText.text = "Prepare Phase";
                yield return new WaitForSeconds(8f);
                isFirstWave = false;
            }

            waveText.text = "War Time";

            foreach (var kvp in waveData)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    GameObject whale = Instantiate(kvp.Key, transform.position, Quaternion.identity);
                    whale.AddComponent<WhaleDestroyHandler>().SetSpawner(this);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            float timer = 0f;
            while (remainingWhales > 0 && timer < 10f)
            {
                yield return new WaitForSeconds(1f);
                timer += 1f;
            }

            while (remainingWhales > 0 && waves.Count == 0)
            {
                yield return new WaitForSeconds(1f);
                timer += 1f;
            }

            if (waves.Count == 0)
            {
                PlayerAttributes player = FindFirstObjectByType<PlayerAttributes>();
                if (player.hp > 0)
                {
                    gameClearText.text = "GameClear!";
                }
            }
        }

        waveText.text = "";
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
