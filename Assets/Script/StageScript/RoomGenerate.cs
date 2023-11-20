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

    public List<GameObject> itemList; // 생성된 아이템 오브젝트들

    [Header("Unity Setup")]
    public EnemyGenerate enemyGenerate;
    public RoomPattern pattern; // 오브젝트 패턴
    public Transform roomPool; // 방 한곳에 모아둘 오브젝트
    public GameObject[] rooms; // room prefabs에 스테이지별로 담아줄 오브젝트
    public GameObject[] objectPrefabs; // 방생성후 오브젝트 생성할때 사용할 프리팹들
    public GameObject[] doorPrefabs;   // 방생성후 문 생성할때 사용할 프리팹들

    [Header("Pooling")]
    public Transform rockPool_Transform;
    Stack<GameObject> rockPool = new Stack<GameObject>();

    public Transform poopPool_Transform;
    Stack<GameObject> poopPool = new Stack<GameObject>();

    public Transform firePool_Transform;
    Stack<GameObject> firePool = new Stack<GameObject>();

    public Transform spikePool_Transform;
    Stack<GameObject> spikePool = new Stack<GameObject>();

    public Transform normalChestPool_Transform;
    Stack<GameObject> normalChestPool = new Stack<GameObject>();

    public Transform goldchestPool_Transform;
    Stack<GameObject> goldChestPool = new Stack<GameObject>();

    public Transform shopTable_Transform;
    Stack<GameObject> shopTablePool = new Stack<GameObject>();

    public Transform goldTable_Transform;
    Stack<GameObject> goldTablePool = new Stack<GameObject>();

    public Transform curseChestPool_Transform;
    Stack<GameObject> curseChestPool = new Stack<GameObject>();
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

        for (int i = 0; i < 5; i++)
        {
            // 오브젝트 생성
            GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
            GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
            GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity); 
            GameObject spike = Instantiate(objectPrefabs[3], spikePool_Transform.position, Quaternion.identity);
            GameObject shopTable = Instantiate(objectPrefabs[5], shopTable_Transform.position, Quaternion.identity);
            GameObject goldTable = Instantiate(objectPrefabs[6], goldTable_Transform.position, Quaternion.identity);
            GameObject normalChest = Instantiate(objectPrefabs[7], normalChestPool_Transform.position, Quaternion.identity);
            GameObject goldChest = Instantiate(objectPrefabs[8], normalChestPool_Transform.position, Quaternion.identity);
            GameObject curseChest = Instantiate(objectPrefabs[10], curseChestPool_Transform.position, Quaternion.identity);

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
            rock.transform.SetParent(rockPool_Transform);
            poop.transform.SetParent(poopPool_Transform);
            fire.transform.SetParent(firePool_Transform);
            spike.transform.SetParent(spikePool_Transform);
            shopTable.transform.SetParent(shopTable_Transform);
            goldTable.transform.SetParent(goldTable_Transform);
            normalChest.transform.SetParent(normalChestPool_Transform);
            goldChest.transform.SetParent(goldchestPool_Transform);
            curseChest.transform.SetParent(curseChestPool_Transform);

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
                    GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
                    rockPool.Push(rock);
                    rock.transform.SetParent(rockPool_Transform);
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
                    GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
                    poopPool.Push(poop);
                    poop.transform.SetParent(poopPool_Transform);
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
                    GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity);
                    firePool.Push(fire);
                    fire.transform.SetParent(firePool_Transform);
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
                    GameObject spike = Instantiate(objectPrefabs[3], spikePool_Transform.position, Quaternion.identity);
                    spikePool.Push(spike);
                    spike.transform.SetParent(spikePool_Transform);
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
                    GameObject normalChest = Instantiate(objectPrefabs[7], normalChestPool_Transform.position, Quaternion.identity);
                    normalChestPool.Push(normalChest);
                    normalChest.transform.SetParent(normalChestPool_Transform);
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
                    GameObject goldChest = Instantiate(objectPrefabs[8], goldchestPool_Transform.position, Quaternion.identity);
                    goldChestPool.Push(goldChest);
                    goldChest.transform.SetParent(goldchestPool_Transform);
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
                    GameObject curseChest = Instantiate(objectPrefabs[10], curseChestPool_Transform.position, Quaternion.identity);
                    curseChestPool.Push(curseChest);
                    curseChest.transform.SetParent(curseChestPool_Transform);
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
        // 돌 0
        for(int i = 0; i < rockPool_Transform.childCount; i++)
        {
            GameObject obj = rockPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<Rock>().ResetObject();
                rockPool.Push(obj);
            }
        }
         
        // 똥 1
        for (int i = 0; i < poopPool_Transform.childCount; i++)
        {
            GameObject obj = poopPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<Poop>().ResetObject();
                poopPool.Push(obj);
            }
        }

        // 불 2
        for (int i = 0; i < firePool_Transform.childCount; i++)
        {
            GameObject obj = firePool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<FirePlace>().ResetObject();
                firePool.Push(obj);
            }
        }

        // 가시 3
        for (int i = 0; i < spikePool_Transform.childCount; i++)
        {
            GameObject obj = spikePool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<Spikes>().ResetObject();
                spikePool.Push(obj);
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

        // 일반 상자 7
        for (int i = 0; i < normalChestPool_Transform.childCount; i++)
        {
            GameObject obj = normalChestPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<NormalChest>().ResetObject();
                normalChestPool.Push(obj);
            }
        }

        // 황금 상자 8
        for (int i = 0; i < goldchestPool_Transform.childCount; i++)
        {
            GameObject obj = goldchestPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<GoldChest>().ResetObject();
                goldChestPool.Push(obj);
            }
        }

        // 저주방 상자 10
        for (int i = 0; i < curseChestPool_Transform.childCount; i++)
        {
            GameObject obj = curseChestPool_Transform.GetChild(i).gameObject;
            if (obj.activeSelf)
            {
                obj.GetComponent<CurseChest>().ResetObject();
                curseChestPool.Push(obj);
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
        roomList = null; // 생성된 방 오브젝트 배열 초기화
        doors = new List<GameObject>(); // 생성된 문 오브젝트 배열 초기화

        AllReturnObject(); // 맵 오브젝트 리턴

        // 생성되어있는 모든 방들을 삭제
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }

        // 생성된 아이템 오브젝트 List 초기화
        // 아이템 리스트가 비어있을때
        if (itemList == null)
        {
            itemList = new List<GameObject>();
        }

        // 아이템 리스트가 비어있지않을때.
        else 
        {
            // 생성되어있는 아이템이있을때 전부 삭제 후 초기화
            for(int i = 0; i < itemList.Count; i++)
            {
                Destroy(itemList[i]);
                // 드랍아이템은 오브젝트 풀링적용시켜서 돌려주고,
                // 그외 액티브,패시브,장신구 아이템은 삭제해야함.
            }
            itemList = new List<GameObject>();
        }

        // 오브젝트 풀링이 적용된( 드랍 아이템 ) 초기화
        ItemManager.instance.itemTable.AllReturnDropItem();
        
    }
    public void CreateRoom(int stage, int size)
    {
        roomList = new GameObject[size, size];

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
                roomList[i, j] = room; 
                roomList[i, j].GetComponent<Room>().isClear = false; // 해당 방을 미클리어 상태로 전환

                // (4 상점) (5 황금) (6 저주) (7 시작) 인 경우 바로 클리어 상태로 전환
                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].GetComponent<Room>().isClear = true;
                

                // 방 오브젝트 생성
                roomList[i, j].GetComponent<Room>().SetGrid(); //
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

            GameObject door = Instantiate(doorPrefabs[doorNumder]);
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

            // 도어 입장 데미지.
            if (doorNumder == 4)
                door.GetComponent<Door>().doorDamage = 1;
            else
                door.GetComponent<Door>().doorDamage = 0;
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
                    Transform pos = roomList[y, x].GetComponent<Room>().roomObjects[idx].transform;
                    GameManager.instance.playerObject.transform.position = pos.position;
                }

                else if (pNum == 5) // 몬스터 오브젝트일때
                {
                    // 랜덤한 일반몬스터를 반환받음.
                    GameObject enemy = enemyGenerate.GetEnemy();
                    enemy.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                    enemy.transform.localPosition = new Vector3(0, 0, 0);
                    enemy.GetComponent<TEnemy>().roomInfo = roomList[y, x];
                    roomList[y, x].GetComponent<Room>().enemis.Add(enemy); // 해당 방의 몬스터리스트에 추가

                    // sfx 사운드 조절을 위한 오브젝트 저장
                    SetSFXDestoryObject(enemy);
                }

                else // 돌 똥 불 가시 아이템상자 3종 , 황금방/보스방 보상 테이블 , 상점방 아이템 테이블
                {
                    GameObject obstacle = GetObstacle(pNum-1);
                    Vector3 pos = roomList[y, x].GetComponent<Room>().roomObjects[idx].position;
                    obstacle.transform.position = pos;

                    // 황금방 테이블
                    if(pNum == 7)
                    {
                        obstacle.GetComponent<GoldTable>().SetRoomInfo(roomList[y, x]);
                    }

                    // 상점방 테이블
                    else if(pNum == 6)
                    {
                        obstacle.GetComponent<ShopTable>().SetRoomInfo(roomList[y, x]);
                    }

                    // 저주방 상자
                    else if (pNum == 11)
                    {
                        obstacle.GetComponent<CurseChest>().SetRoomInfo(roomList[y, x]);
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
        {
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());
        }
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
                doors[i].GetComponent<Door>().CheckedClear();
            }
        }
    }
}
