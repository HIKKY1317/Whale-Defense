using UnityEngine;
using UnityEngine.UI;

public class TarotPrefabSelector : MonoBehaviour
{
    public GameObject miniTarotPrefab;
    public GameObject tarotPrefab;
    public GameObject largeTarotPrefab;

    private MapCreator mapCreator;

    void Start()
    {
        mapCreator = FindFirstObjectByType<MapCreator>();
        if (mapCreator == null)
        {
            Debug.LogError("");
        }
    }

    public void SelectMiniTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = miniTarotPrefab;
        }
    }

    public void SelectTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = tarotPrefab;
        }
    }

    public void SelectLargeTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = largeTarotPrefab;
        }
    }
}
