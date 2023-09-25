using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    // 1: 矫累规  2:老馆规  3:焊胶规  4:惑痢规  5:炔陛规  6:历林规
    public int[,] stageArr;
    public bool CreateStage(int size, int min)
    {
        stageArr = new int[size, size];

        if (CreateStructure(size, min))
        {
            if (SelectRoom(size))
            {
                return true;
            }
        }
        return false;
    }

    private bool SelectRoom(int size)
    {
        int roomNum = 3;

        List<KeyValuePair<int, int>> temp = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // null or startRoom
                if (stageArr[i, j] == 0 || stageArr[i, j] == 1)
                    continue;

                int adj = CheckAbjCount(i, j, size);
                if (adj == 1)
                    temp.Add(new KeyValuePair<int, int>(i, j));
            }
        }

        if (temp.Count >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                        continue;
                }

                int rd = Random.Range(0, temp.Count);
                stageArr[temp[rd].Key, temp[rd].Value] = roomNum;
                roomNum++;
                temp.RemoveAt(rd);
            }

            return true;
        }
        return false;
    }

    bool CreateStructure(int size, int min)
    {
        int roomCount = 1;
        int midY = size / 2;
        int midX = size / 2;

        stageArr[midY, midX] = 1;
        Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>();
        q.Enqueue(new KeyValuePair<int, int>(midY, midX));
        while (q.Count != 0)
        {
            KeyValuePair<int, int> qFront = q.Dequeue();

            int y = qFront.Key;
            int x = qFront.Value;

            for (int i = 0; i < 4; i++)
            {
                int ny = y + dy[i];
                int nx = x + dx[i];

                if (ny < 0 || nx < 0 || ny >= size || nx >= size)
                    continue;

                if (stageArr[ny, nx] == 0)
                {
                    int adjCnt = CheckAbjCount(ny, nx, size);
                    if (adjCnt >= 2)
                        continue;

                    int rd = Random.Range(0, 3);
                    if (rd == 0)
                        continue;

                    stageArr[ny, nx] = 2;
                    roomCount++;
                    q.Enqueue(new KeyValuePair<int, int>(ny, nx));
                }
            }
        }
        if (roomCount >= min)
            return true;
        return false;

    }

    private int CheckAbjCount(int y, int x, int size)
    {
        int ret = 0;

        for (int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            if (ny < 0 || nx < 0 || ny >= size || nx >= size)
                continue;

            if (stageArr[ny, nx] == 0)
                continue;

            ret++;
        }

        return ret;
    }
}
