using System.Collections.Generic;
using UnityEngine;

public class MapRouteCalculator : MonoBehaviour
{
    private char[,] map;
    private List<Vector2Int> route = new List<Vector2Int>();

    private readonly Vector2Int[] directions =
    {
        new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0),
        new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1)
    };

    public void SetMapData(char[,] receivedMap)
    {
        map = receivedMap;
        FindRoute();
    }

    private void FindRoute()
    {
        Vector2Int start = new Vector2Int(-1, -1);
        Vector2Int goal = new Vector2Int(-1, -1);

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 'S') start = new Vector2Int(i, j);
                if (map[i, j] == 'H') goal = new Vector2Int(i, j);
            }
        }

        if (start.x == -1 || goal.x == -1)
        {
            Debug.LogError("スタートまたはゴールが見つかりません。");
            return;
        }

        // BFS
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal) break;

            foreach (var dir in directions)
            {
                Vector2Int next = current + dir;

                if (IsValidPosition(next, rows, cols) && !visited.Contains(next) && map[next.x, next.y] != '#')
                {
                    if (Mathf.Abs(dir.x) + Mathf.Abs(dir.y) == 2 && !IsValidDiagonalMove(current, next))
                    {
                        continue;
                    }

                    queue.Enqueue(next);
                    visited.Add(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
        {
            Debug.LogError("ゴールに到達できません。");
            return;
        }

        route.Clear();
        Vector2Int step = goal;
        while (step != start)
        {
            route.Add(step);
            step = cameFrom[step];
        }
        route.Add(start);
        route.Reverse();

        SendRouteToMapRoute();
    }

    private bool IsValidPosition(Vector2Int pos, int rows, int cols)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
    }

    private bool IsValidDiagonalMove(Vector2Int current, Vector2Int next)
    {
        return map[next.x, current.y] != '#' && map[current.x, next.y] != '#';
    }

    private void SendRouteToMapRoute()
    {
        MapRoute mapRoute = FindFirstObjectByType<MapRoute>();
        if (mapRoute != null)
        {
            mapRoute.SetRoute(route);
        }
        else
        {
            Debug.LogError("MapRouteが見つかりません。シーンに配置してください。");
        }
    }
}
