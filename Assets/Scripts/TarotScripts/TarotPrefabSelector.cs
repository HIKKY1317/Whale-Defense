using UnityEngine;

public class TarotPrefabSelector : MonoBehaviour
{
    public GameObject miniTarotPrefab;
    public GameObject tarotPrefab;
    public GameObject largeTarotPrefab;

    private MapCreator mapCreator;

    public int miniTarotCost = 10;
    public int tarotCost = 20;
    public int largeTarotCost = 30;

    public GameObject selectedTarotPrefab { get; private set; }
    public int selectedTarotCost { get; private set; }

    void Start()
    {
        mapCreator = FindFirstObjectByType<MapCreator>();
        if (mapCreator == null)
        {
            Debug.LogError("");
        }

        SelectTarot();
    }

    public void SelectMiniTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = miniTarotPrefab;
            selectedTarotPrefab = miniTarotPrefab;
            selectedTarotCost = miniTarotCost;
        }
    }

    public void SelectTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = tarotPrefab;
            selectedTarotPrefab = tarotPrefab;
            selectedTarotCost = tarotCost;
        }
    }

    public void SelectLargeTarot()
    {
        if (mapCreator != null)
        {
            mapCreator.tarotPrefab = largeTarotPrefab;
            selectedTarotPrefab = largeTarotPrefab;
            selectedTarotCost = largeTarotCost;
        }
    }
}
