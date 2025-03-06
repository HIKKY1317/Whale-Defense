using UnityEngine;
using System.Collections.Generic;

public class MapPathChecker : MonoBehaviour
{
    private char[,] map;
    private int width, height;

    public void SetMapData(char[,] receivedMap)
    {
        map = receivedMap;
        height = map.GetLength(0);
        width = map.GetLength(1);
    }

    public bool BlocksPath(int h, int w)
    {
        char[,] tempMap = (char[,])map.Clone();
        tempMap[h, w] = 'T';

        return !IsPathAvailable(tempMap);
    }

    private bool IsPathAvailable(char[,] tempMap)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (tempMap[i, j] == 'S')
                {
                    queue.Enqueue(new Vector2Int(i, j));
                    visited.Add(new Vector2Int(i, j));
                }
            }
        }

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (tempMap[current.x, current.y] == 'H')
            {
                return true;
            }

            foreach (var dir in directions)
            {
                Vector2Int next = current + dir;

                if (IsWithinBounds(next.x, next.y) &&
                    !visited.Contains(next) &&
                    tempMap[next.x, next.y] != '#' &&
                    tempMap[next.x, next.y] != 'T')

                {
                    queue.Enqueue(next);
                    visited.Add(next);
                }
            }
        }

        return false;
    }

    private bool IsWithinBounds(int h, int w)
    {
        return h>= 0 && h < height && w >= 0 && w < width;
    }
}
