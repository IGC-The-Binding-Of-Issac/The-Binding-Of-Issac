using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    List<GameObject> doors; // 생성된 문 오브젝트들

    public GameObject[,] roomList; // 생성된 방 오브젝트들
    
    public GameObject[,] roomPrefabs; // 방생성에 사용할 프래핍

    [Header("Unity Setup")]
    public EnemyGenerate enemyGenerate;
    public RoomPattern pattern; // 오브젝트 패턴
    public Transform roomPool; // 방 한곳에 모아둘 오브젝트
    public GameObject[] rooms; // room prefabs에 스테이지별로 담아줄 오브젝트
    public GameObject[] objectPrefabs; // 방생성후 오브젝트 생성할때 사용할 프리팹들
    public GameObject[] doorPrefabs;   // 방생성후 문 생성할때 사용할 프리팹들

    public void SetPrefabs()
    {
        /* 
         * roomPrefabs[ 스테이지 - 1 , 방번호 - 1 ]
         * (-1 : 생성X)  (0 : 시작방)  (1 : 일반방)  (2 : 보스방)  (3 : 상점방)  (4 : 황금방) (5 : 저주방)
         * 
         * objects[]
         * (0 : 돌)  (1 : 똥)  (2 : 모닥불)  (3 : 구덩이)
         * 
         * doorPrefabs[]
         * (0 : 일반방)  (1 : 보스방)  (2 : 상점방)  (3 : 골드방)  (4 : 저주방)
         */
        int cnt = 0;
        roomPrefabs = new GameObject[4, 6];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                roomPrefabs[i, j] = rooms[cnt++];
            }
        }
    }
    public void ClearRoom()
    {
        roomList = null;
        doors = new List<GameObject>();
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }
    }
    public void CreateRoom(int stage, int size)
    {
        roomList = new GameObject[size, size];

        Vector3 roomPos = new Vector3(0, 0, 0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j];
                if (roomNum == 0)
                {
                    roomPos += new Vector3(15, 0, 0);
                    continue;
                }
                // create room
                GameObject room = Instantiate(roomPrefabs[stage - 1, roomNum - 1], roomPos, Quaternion.identity) as GameObject;
                roomList[i, j] = room;
                roomList[i, j].GetComponent<Room>().isClear = false;

                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].GetComponent<Room>().isClear = true;
                

                // create obstacle
                roomList[i, j].GetComponent<Room>().SetGrid();
                CreateObstacle(i, j, roomNum);
                room.transform.SetParent(roomPool);

                roomPos += new Vector3(15, 0, 0);
            }
            roomPos = new Vector3(0, roomPos.y, 0);
            roomPos += new Vector3(0, -10, 0);
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j];
                if (roomNum == 0)
                {
                    continue;
                }
                CreateDoor(i, j);
            }
        }
    }
    public void CreateDoor(int y, int x)
    {
        for (int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            // out of range
            if (ny < 0 || nx < 0 || ny >= GameManager.instance.stageSize || nx >= GameManager.instance.stageSize)
                continue;

            // nextRoom  Null 
            if (GameManager.instance.stageGenerate.stageArr[ny, nx] == 0)
                continue;

            // 문 선택
            int doorNumder = ChoiceDoor(y,x,ny,nx);

            GameObject door = Instantiate(doorPrefabs[doorNumder]) as GameObject;
            door.transform.SetParent(roomList[y, x].GetComponent<Room>().doorPosition[i]);
            door.transform.localPosition = new Vector3(0, 0, 0);
            door.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            door.GetComponent<Door>().doorDir = i; // door dir

            door.GetComponent<Door>().roomInfo = roomList[y, x]; // door in Room Info
            door.GetComponent<Door>().movePosition = roomList[ny, nx].GetComponent<Room>().movePosition[i]; // door MovePosition 

            int roomNum = GameManager.instance.stageGenerate.stageArr[y, x];
            if (roomNum == 4 || roomNum == 5 || roomNum == 6)
                door.GetComponent<Door>().UsingKey();

            doors.Add(door);
        }
    }
    int ChoiceDoor(int y, int x, int ny, int nx)
    {
        int doorNum;
        int roomNum = GameManager.instance.stageGenerate.stageArr[y, x];
        int nextRoomNum = GameManager.instance.stageGenerate.stageArr[ny, nx];
        // 현재방이 보스,상점,황금,저주 일때
        if (3 <= roomNum && roomNum <= 6)
        {
            doorNum = roomNum - 2;
        }
        // 현재방이 시작, 일반 일때
        else
        {
            // 다음방이 보스,상점,황금,저주 일때
            if (3 <= nextRoomNum && nextRoomNum <= 6)
            {
                doorNum = nextRoomNum - 2;
            }
            else
            {
                doorNum = 0;
            }
        }

        return doorNum;
    }


    void CreateObstacle(int y, int x, int roomNumber)
    {
        int idx = 0;
        int[,] rdPattern = pattern.GetPattern(roomNumber); 
        for(int i = 0; i < rdPattern.GetLength(0); i++)
        {
            for(int j = 0; j < rdPattern.GetLength(1); j++)
            {
                if (rdPattern[i, j] == 0)
                {
                    idx++;
                    continue;
                }

                if (rdPattern[i, j] == 5) // 몬스터 오브젝트일때
                {
                    // 랜덤한 일반몬스터를 반환받음.
                    GameObject enemy = enemyGenerate.GetEnemy();
                    enemy.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                    enemy.transform.localPosition = new Vector3(0, 0, 0);
                    enemy.GetComponent<Enemy>().roomInfo = roomList[y, x];
                    roomList[y, x].GetComponent<Room>().enemis.Add(enemy); // 해당 방의 몬스터리스트에 추가
                }
                else
                {
                    GameObject obstacle = Instantiate(objectPrefabs[rdPattern[i, j] - 1]) as GameObject;
                    obstacle.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                    obstacle.transform.localPosition = new Vector3(0, 0, 0);
                    if (rdPattern[i, j] == 10) // 플레이어 오브젝트일때
                    {
                        obstacle.transform.SetParent(null);
                        GameManager.instance.playerObject = obstacle;
                    }
                }
                idx++;
            }
        }    
    }



    private void Update()
    {
        if(doors != null)
        {
            for(int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Door>().CheckedClear();
            }
        }
    }

}
