using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    // 1: 시작방  2:일반방  3:보스방  4:상점방  5:황금방  6:저주방
    private int[,] stagearr;
    public int[,] stageArr {  get { return stagearr; } }
    public bool CreateStage(int size, int min)
    {
        stagearr = new int[size, size]; // 스테이지 구조를 2차원 배열로 선언

        if (CreateStructure(size, min)) // 구조 생성
        {
            // 구조 생성에 성공했을때 플레이에 필요한 방들을 지정.
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

        // 모든 방을 탐색
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // 비어있는방 또는 시작방을 제외
                if (stageArr[i, j] == 0 || stageArr[i, j] == 1)
                    continue;

                // 인접한방의 개수 확인.
                int adj = CheckAbjCount(i, j, size);
                // 인접한 방의 개수가1개일때 ( 특수방 생성 가능 조건 )
                if (adj == 1)
                    temp.Add(new KeyValuePair<int, int>(i, j)); // 위치를 저장
            }
        }

        // 특수방을 생성할수있는 방의 개수가 4개 이상일때
        if (temp.Count >= 4)
        {
            // 황금방,상점방,보스방,저주방을 지정
            for (int i = 0; i < 4; i++)
            {
                // 저주방 생성
                if (i == 3)
                {
                    // 대충 50%확률로 생성 또는 생성X
                    int r = Random.Range(0, 2);
                    if (r == 0)
                        continue;
                }

                // 인접한 방의 개수가1개인 방들중 랜덤한 방들을
                // 보스방 상점방 황금방 저주방 순서대로 지정.
                int rd = Random.Range(0, temp.Count);
                stageArr[temp[rd].Key, temp[rd].Value] = roomNum;
                roomNum++;
                temp.RemoveAt(rd);
            }
            return true;
        }

        // 특수방을 생성할수있는 방이 3개 이하면 구조 생성 실패.
        return false;
    }

    bool CreateStructure(int size, int min)
    {
        int roomCount = 1; // 현재  생성된 방 개수 
        int midY = size / 2; // 중앙 위치 ( 시작방 )
        int midX = size / 2; // 중앙 위치 ( 시작방 )

        stageArr[midY, midX] = 1; // 스테이지의 중앙을 시작방으로 지정
        Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>(); // BFS으로 구조를 생성하기위한 Queue 선언
        q.Enqueue(new KeyValuePair<int, int>(midY, midX)); // 시작방부터 시작하기 위해 시작방 위치 push
        while (q.Count != 0)
        {
            KeyValuePair<int, int> qFront = q.Dequeue();

            int y = qFront.Key; // 현재위치 y
            int x = qFront.Value; // 현재위치 x

            for (int i = 0; i < 4; i++) // 상하좌우
            {
                int ny = y + dy[i]; // 다음위치 y
                int nx = x + dx[i]; // 다음위치 x

                if (ny < 0 || nx < 0 || ny >= size || nx >= size) // 범위 벗어났을때
                    continue;

                if (stageArr[ny, nx] == 0) // 아직 지정되지 않은 방일때
                {
                    int adjCnt = CheckAbjCount(ny, nx, size); // ny nx 방이랑 인접해있는 방의 개수
                    if (adjCnt >= 2) // 인접해있는 방의 개수가 2개 이상일때
                        continue;  // pass

                    // 인접해있는 방의 개수가 1개 이하일때
                    int rd = (Random.Range(0, 101) % 3); //일정 확률로 방생성 여부를 정함.
                    if (rd == 0) 
                        continue; // pass

                    stageArr[ny, nx] = 2; // ny nx 방을 일반방으로 지정
                    roomCount++; // 생성된 방 개수 ++
                    q.Enqueue(new KeyValuePair<int, int>(ny, nx));  // 이후 추가 생성을 위한 push
                }
            }
        }

        // 구조 생성을 완료하였을때
        // 생성된 방의 개수가 최소방개수이상 생성되었다면 생성 성공
        if (roomCount >= min)
            return true;
        return false;
    }
    
    private int CheckAbjCount(int y, int x, int size)
    {
        int ret = 0;

        for (int i = 0; i < 4; i++) // 상하좌우 탐색
        {
            int ny = y + dy[i]; 
            int nx = x + dx[i];

            if (ny < 0 || nx < 0 || ny >= size || nx >= size) // 범위 벗어났을때 x
                continue;

            if (stageArr[ny, nx] == 0) // 비어있는방일때 
                continue;

            // 스테이지 범위 내에 있고, 방이 존재할때 ++
            ret++;
        }

        return ret; // 인접해잇는 방 개수 리턴.
    }
}
