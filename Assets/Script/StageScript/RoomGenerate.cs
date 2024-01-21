using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    List<Door> doors; // 생성된 문 오브젝트들

    private Room[,] roomlist; // 생성된 방 오브젝트들
    public Room[,] roomList { get => roomlist; }
    
    public GameObject[,] roomPrefabs; // 방생성에 사용할 프래핍

    public List<GameObject> itemList; // 생성된 아이템 오브젝트들

    [Header("Unity Setup")]
    public EnemyGenerate enemyGenerate;
    public RoomPattern pattern; // 오브젝트 패턴
    public Transform roomPool; // 방 한곳에 모아둘 오브젝트
    public GameObject[] rooms; // room prefabs에 스테이지별로 담아줄 오브젝트
    public GameObject[] objectPrefabs; // 방생성후 오브젝트 생성할때 사용할 프리팹들
    public GameObject[] doorPrefabs;   // 방생성후 문 생성할때 사용할 프리팹들

    [Header("Pooling")]
    // 맵 오브젝트
    public Transform obstaclePool_Transform;
    private Stack<GameObject> rockPool = new Stack<GameObject>();
    private Stack<GameObject> poopPool = new Stack<GameObject>();
    private Stack<GameObject> firePool = new Stack<GameObject>();
    private Stack<GameObject> spikePool = new Stack<GameObject>();
    public Stack<GameObject> RockPool { get => rockPool; }
    public Stack<GameObject> PoopPool { get => poopPool; }
    public Stack<GameObject> FirePool { get => firePool; }
    public Stack<GameObject> SpikePool { get => spikePool; }

    // 상자 
    public Transform chestPool_Transform;
    Stack<GameObject> normalChestPool = new Stack<GameObject>();
    Stack<GameObject> goldChestPool = new Stack<GameObject>();
    Stack<GameObject> curseChestPool = new Stack<GameObject>();
    public Stack<GameObject> NormalChestPool { get => normalChestPool; }
    public Stack<GameObject> GoldChestPool { get => goldChestPool; }
    public Stack<GameObject> CurseChestPool { get => curseChestPool; }


    // 상점/황금방 보상 테이블
    public Transform shopTable_Transform;
    Stack<GameObject> shopTablePool = new Stack<GameObject>();

    public Transform goldTable_Transform;
    Stack<GameObject> goldTablePool = new Stack<GameObject>();

    public void SetObjectPooling()
    {
        rockPool = new Stack<GameObject>();
        poopPool = new Stack<GameObject>();
        firePool = new Stack<GameObject>();
        spikePool = new Stack<GameObject>();
        normalChestPool = new Stack<GameObject>();
        goldChestPool = new Stack<GameObject>();
        goldTablePool = new Stack<GameObject>();
        shopTablePool = new Stack<GameObject>();
        curseChestPool = new Stack<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            // 오브젝트 생성
            GameObject rock = Instantiate(objectPrefabs[0], obstaclePool_Transform.position, Quaternion.identity);
            GameObject poop = Instantiate(objectPrefabs[1], obstaclePool_Transform.position, Quaternion.identity);
            GameObject fire = Instantiate(objectPrefabs[2], obstaclePool_Transform.position, Quaternion.identity); 
            GameObject spike = Instantiate(objectPrefabs[3], obstaclePool_Transform.position, Quaternion.identity);

            GameObject shopTable = Instantiate(objectPrefabs[5], shopTable_Transform.position, Quaternion.identity);
            GameObject goldTable = Instantiate(objectPrefabs[6], goldTable_Transform.position, Quaternion.identity);

            GameObject normalChest = Instantiate(objectPrefabs[7], chestPool_Transform.position, Quaternion.identity);
            GameObject goldChest = Instantiate(objectPrefabs[8], chestPool_Transform.position, Quaternion.identity);
            GameObject curseChest = Instantiate(objectPrefabs[10], chestPool_Transform.position, Quaternion.identity);

            // 오브젝트 풀 담아주기
            rockPool.Push(rock);
            poopPool.Push(poop);
            firePool.Push(fire);
            spikePool.Push(spike);

            shopTablePool.Push(shopTable);
            goldTablePool.Push(goldTable);

            normalChestPool.Push(normalChest);
            goldChestPool.Push(goldChest);
            curseChestPool.Push(curseChest);

            // 오브젝트 한곳에 모아두기.
            rock.transform.SetParent(obstaclePool_Transform);
            poop.transform.SetParent(obstaclePool_Transform);
            fire.transform.SetParent(obstaclePool_Transform);
            spike.transform.SetParent(obstaclePool_Transform);

            shopTable.transform.SetParent(shopTable_Transform);
            goldTable.transform.SetParent(goldTable_Transform);

            normalChest.transform.SetParent(chestPool_Transform);
            goldChest.transform.SetParent(chestPool_Transform);
            curseChest.transform.SetParent(chestPool_Transform);

            // 사운드 조절을 위해 SFXObject로 넣어주기 // 이 부분 작동안함.
            SetSFXObject(rock);
            SetSFXObject(poop);
            SetSFXObject(fire);
            SetSFXObject(spike);

            SetSFXObject(shopTable); // 사운드 없는 오브젝트 입니다.
            SetSFXObject(goldTable); // 사운드 없는 오브젝트입니다.

            SetSFXObject(normalChest);
            SetSFXObject(goldChest);
            SetSFXObject(curseChest);

            rock.SetActive(false);
            poop.SetActive(false);
            fire.SetActive(false);
            spike.SetActive(false);

            shopTable.SetActive(false);
            goldTable.SetActive(false);

            normalChest.SetActive(false);
            goldChest.SetActive(false);
            curseChest.SetActive(false);
        }
    }

    GameObject GetObstacle(int num)
    {
        switch(num)
        {
            #region 돌
            case 0: // 돌 
                if(rockPool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject rock = Instantiate(objectPrefabs[0], obstaclePool_Transform.position, Quaternion.identity);
                    rockPool.Push(rock);
                    rock.transform.SetParent(obstaclePool_Transform);
                    SetSFXObject(rock);
                }
                GameObject rockObj = rockPool.Pop();
                rockObj.SetActive(true);
                return rockObj;
            #endregion

            #region 똥
            case 1: // 똥
                if (poopPool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject poop = Instantiate(objectPrefabs[1], obstaclePool_Transform.position, Quaternion.identity);
                    poopPool.Push(poop);
                    poop.transform.SetParent(obstaclePool_Transform);
                    SetSFXObject(poop);
                }
                GameObject poopObj = poopPool.Pop();
                poopObj.SetActive(true);
                return poopObj;
            #endregion

            #region 불
            case 2: // 불
                if (firePool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject fire = Instantiate(objectPrefabs[2], obstaclePool_Transform.position, Quaternion.identity);
                    firePool.Push(fire);
                    fire.transform.SetParent(obstaclePool_Transform);
                    SetSFXObject(fire);
                }
                GameObject fireObj = firePool.Pop();
                fireObj.SetActive(true);
                return fireObj;
            #endregion

            #region 가시
            case 3: // 가시
                if (spikePool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject spike = Instantiate(objectPrefabs[3], obstaclePool_Transform.position, Quaternion.identity);
                    spikePool.Push(spike);
                    spike.transform.SetParent(obstaclePool_Transform);
                    SetSFXObject(spike);
                }
                GameObject spikeObj = spikePool.Pop();
                spikeObj.SetActive(true);
                return spikeObj;
            #endregion

            #region 상점방 아이템테이블
            case 5:
                if (shopTablePool.Count == 0)
                {
                    GameObject shopTable = Instantiate(objectPrefabs[6], shopTable_Transform.position, Quaternion.identity);
                    shopTablePool.Push(shopTable);
                    shopTable.transform.SetParent(shopTable_Transform);
                    SetSFXObject(shopTable); // 사운드가없는 오브젝트입니다.
                }
                GameObject shopTableObj = shopTablePool.Pop();
                shopTableObj.SetActive(true);
                return shopTableObj;
            #endregion

            #region 황금방 보상테이블
            case 6:
                if(goldTablePool.Count == 0)
                {
                    GameObject goldTable = Instantiate(objectPrefabs[6], goldTable_Transform.position, Quaternion.identity);
                    goldTablePool.Push(goldTable);
                    goldTable.transform.SetParent(goldTable_Transform);
                    SetSFXObject(goldTable); // 사운드가없는 오브젝트입니다.
                }
                GameObject goldTableObj = goldTablePool.Pop();
                goldTableObj.SetActive(true);
                return goldTableObj;
                #endregion

            #region 일반상자
            case 7:
                if(normalChestPool.Count == 0)
                {
                    GameObject normalChest = Instantiate(objectPrefabs[7], chestPool_Transform.position, Quaternion.identity);
                    normalChestPool.Push(normalChest);
                    normalChest.transform.SetParent(chestPool_Transform);
                    SetSFXObject(normalChest);
                }
                GameObject normalChestObj = normalChestPool.Pop();
                normalChestObj.SetActive(true);
                return normalChestObj;
            #endregion

            #region 황금상자
            case 8:
                if (goldChestPool.Count == 0)
                {
                    GameObject goldChest = Instantiate(objectPrefabs[8], chestPool_Transform.position, Quaternion.identity);
                    goldChestPool.Push(goldChest);
                    goldChest.transform.SetParent(chestPool_Transform);
                    SetSFXObject(goldChest);
                }
                GameObject goldChestObj = goldChestPool.Pop();
                goldChestObj.SetActive(true);
                return goldChestObj;
            #endregion

            #region 저주방상자
            case 10:
                if (curseChestPool.Count == 0)
                {
                    GameObject curseChest = Instantiate(objectPrefabs[10], chestPool_Transform.position, Quaternion.identity);
                    curseChestPool.Push(curseChest);
                    curseChest.transform.SetParent(chestPool_Transform);
                    SetSFXObject(curseChest);
                }
                GameObject curseChestObj = curseChestPool.Pop();
                curseChestObj.SetActive(true);
                return curseChestObj;
                #endregion
        }
        return null;
    }

    void AllReturnObject()
    {
        // 돌 똥 불 가시
        for(int i = 0; i < obstaclePool_Transform.childCount; i++)
        {
            GameObject obj = obstaclePool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<Obstacle>().ResetObject();
                obj.GetComponent<Obstacle>().Returnobject(); ;
            }
        }

        // 일반상자/ 황금상자/ 저주방상자
        for (int i = 0; i < chestPool_Transform.childCount; i++)
        {
            GameObject obj = chestPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<Chest>().ResetObject();
                obj.GetComponent<Chest>().Returnobject();
            }
        }


        // 상점방 아이템 테이블 5
        for (int i = 0; i < shopTable_Transform.childCount; i++)
        {
            GameObject obj = shopTable_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<ShopTable>().ResetObject();
                shopTablePool.Push(obj);
            }
        }

        // 황금방 보상테이블 6
        for (int i = 0; i < goldTable_Transform.childCount; i++)
        {
            GameObject obj = goldTable_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<GoldTable>().ResetObject();
                goldTablePool.Push(obj);
            }
        }
    }

    public void SetPrefabs()
    {
        /* 
         * roomPrefabs[ 스테이지 - 1 , 방번호 - 1 ]
         * (-1 : 생성X)  (0 : 시작방)  (1 : 일반방)  (2 : 보스방)  (3 : 상점방)  (4 : 황금방) (5 : 저주방)
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
        roomlist = null; // 생성된 방 오브젝트 배열 초기화
        doors = new List<Door>(); // 생성된 문 오브젝트 배열 초기화

        AllReturnObject(); // 맵 오브젝트 초기화

        // 생성되어있는 모든 방들을 삭제
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }

        // 생성된 아이템 오브젝트 List 초기화 ( 액티브 , 장신구, 패시브 아이템 )
        if (itemList == null)
        {
            // LIst가 Null일때 
            itemList = new List<GameObject>();
        }

        // List가 Null이 아닐대
        else 
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                // 생성되어있는 액티브,장신구,패시브 아이템을 삭제
                Destroy(itemList[i]);
            }
            itemList = new List<GameObject>();
        }

        ItemManager.instance.itemTable.AllReturnDropItem(); // 픽업 아이템 초기화
    }
    public void CreateRoom(int stage, int size)
    {
        // 방 오브젝트들을 담는 배열을 구조에맞춰 2차원배열로 선언
        roomlist = new Room[size, size]; 

        Vector3 roomPos = new Vector3(0, 0, 0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j]; // 스테이지 구조에서 방 번호

                // 비어있는방
                if (roomNum == 0)
                {
                    roomPos += new Vector3(15, 0, 0);
                    continue;
                }
                // 그외

                // 방 오브젝트 생성
                GameObject room = Instantiate(roomPrefabs[stage - 1, roomNum - 1], roomPos, Quaternion.identity) as GameObject; // 방 오브젝트를 생성.
                SetSFXDestoryObject(room); // 해당 방을 Sound Manager의 SFXObject로 추가
                roomList[i, j] = room.GetComponent<Room>();
                roomList[i, j].isClear = false; // 방을 미클리어 상태로 전환

                // (4 상점) (5 황금) (6 저주) (1 시작) 인 경우 바로 클리어 상태로 전환
                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].isClear = true;
                

                // 방 오브젝트 생성
                roomList[i, j].SetGrid(); 
                CreateObstacle(i, j, roomNum);  // 맵 오브젝트 / 몬스터 생성
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
                if (roomNum == 0) // 0 : 빈 방
                {
                    continue;
                }
                CreateDoor(i, j); // 다음 방으로 넘어가는 문을 생성
            }
        }
    }
    public void CreateDoor(int y, int x)
    {
        for (int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            // 범위 밖
            if (ny < 0 || nx < 0 || ny >= GameManager.instance.stageSize || nx >= GameManager.instance.stageSize)
                continue;

            // 다음방이 없을때.
            if (GameManager.instance.stageGenerate.stageArr[ny, nx] == 0)
                continue;

            // 문 선택
            int doorNumder = ChoiceDoor(y,x,ny,nx);

            GameObject door = Instantiate(doorPrefabs[doorNumder]);
            Door doorComponent = door.GetComponent<Door>();
            door.transform.SetParent(roomList[y, x].GetComponent<Room>().doorPosition[i]);
            door.transform.localPosition = new Vector3(0, 0, 0);
            door.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            doorComponent.doorDir = i; // 문 방향

            doorComponent.roomInfo = roomList[y, x]; // 생성된 문에 방 정보 추가.
            doorComponent.movePosition = roomList[ny, nx].movePosition[i]; // 문을 사용했을때 이동하는 위치

            int roomNum = GameManager.instance.stageGenerate.stageArr[y, x];
            if (roomNum == 4 || roomNum == 5 || roomNum == 6)
                doorComponent.UsingKey();

            doors.Add(doorComponent);

            // 도어 입장 데미지.
            if (doorNumder == 4)
                doorComponent.doorDamage = 1;
            else
                doorComponent.doorDamage = 0;
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
        int[,] rdPattern = pattern.GetPattern(roomNumber); // 방 패턴을 반환받음.
        for (int i = 0; i < rdPattern.GetLength(0); i++) 
        {
            for(int j = 0; j < rdPattern.GetLength(1); j++)
            {
                int pNum = rdPattern[i, j]; ;
                if (pNum == 0) // 빈칸
                {
                    idx++;
                    continue;
                }

                else if (pNum == 10) // 플레이어 오브젝트일때
                {
                    // 플레이어는 생성X , 이전에 생성해둔 플레이어 오브젝트를 이동시킴.
                    Transform pos = roomList[y, x].roomObjects[idx].transform;
                    GameManager.instance.playerObject.transform.position = pos.position;
                    GameManager.instance.miniMapPosition = pos;
                }

                else if (pNum == 5) // 몬스터 오브젝트일때
                {
                    // 랜덤한 일반몬스터를 반환받음.
                    GameObject enemy = enemyGenerate.GetEnemy();
                    enemy.transform.SetParent(roomList[y, x].roomObjects[idx]);
                    enemy.transform.localPosition = new Vector3(0, 0, 0);
                    enemy.GetComponent<TEnemy>().roomInfo = roomList[y, x].gameObject;
                    roomList[y, x].enemis.Add(enemy); // 해당 방의 몬스터리스트에 추가

                    // sfx 사운드 조절을 위한 오브젝트 저장
                    SetSFXDestoryObject(enemy);
                }

                else // 돌 똥 불 가시 아이템상자 3종 , 황금방/보스방 보상 테이블 , 상점방 아이템 테이블
                {
                    GameObject obstacle = GetObstacle(pNum-1);
                    Vector3 pos = roomList[y, x].roomObjects[idx].position;
                    obstacle.transform.position = pos;

                    // 황금방 테이블
                    if(pNum == 7)
                    {
                        obstacle.GetComponent<GoldTable>().SetRoomInfo(roomList[y, x].gameObject);
                    }

                    // 상점방 테이블
                    else if(pNum == 6)
                    {
                        obstacle.GetComponent<ShopTable>().SetRoomInfo(roomList[y, x].gameObject);
                    }

                    // 저주방 상자
                    else if (pNum == 11)
                    {
                        obstacle.GetComponent<CurseChest>().SetRoomInfo(roomList[y, x].gameObject);
                    }
                }
                idx++;
            }
        }    
    }

    void SetSFXObject(GameObject obj)
    {
        // sfx 사운드 조절을 위한 오브젝트 저장
        if (obj.GetComponent<AudioSource>() != null)
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());
    }

    public void SetSFXDestoryObject(GameObject obj)
    {
        // sfx 사운드 조절을 위한 오브젝트 저장
        if (obj.GetComponent<AudioSource>() != null)
            SoundManager.instance.sfxDestoryObjects.Add(obj.GetComponent<AudioSource>());
    }

    private void Update()
    {
        if(doors != null)
        {
            for(int i = 0; i < doors.Count; i++)
            {
                doors[i].CheckedClear();
            }
        }
    }
}