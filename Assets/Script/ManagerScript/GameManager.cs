using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleTon
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    public int stageLevel; // 현재 스테이지 레벨
    public int stageSize;  // 스테이지 크기
    public int stageMinimunRoom; // 스테이지 방 최소개수
    public GameObject playerObject; // 생성된 플레이어

    [Header("Unity Setup")]
    public StageGenerate stageGenerate;
    public RoomGenerate roomGenerate;
    public GameObject myCamera;

    private void Start()
    {
        SetStage(1); // 1stage create
        roomGenerate.SetPrefabs(); // room Prefabs Setting
    }

    void Update()
    {
        // 스테이지 생성 테스트.
        // R키 누르기 -> 스테이지 시작시로 변경 할것. 
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            StageStart();
        }
    }
    public void StageStart()
    {
        // Create stage/room

        if (playerObject != null)
            Destroy(playerObject);
        myCamera.transform.SetParent(null);

        int cnt = 10;
        while (cnt-- > 0)
        {
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom))
            {
                roomGenerate.ClearRoom(); // room reset -> create
                roomGenerate.CreateRoom(stageLevel, stageSize); // room create
                break;
            }
        }
    }

    public void SetStage(int stage)
    {
        stageLevel = stage;
        switch(stageLevel)
        {
            case 1: // 1스테이지
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
            case 2: // 2스테이지
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
            case 3: // 3스테이지
                stageSize = 7;
                stageMinimunRoom = 10;
                break;
            case 4: // 4스테이지
                stageSize = 7;
                stageMinimunRoom = 12;
                break;
            default:
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
        }
    }

    // 사망시 호출
    // 플레이어 사망함수에 추가해줄것
    void GameOver()
    {
        // 게임 오버 UI 출력후 
        // 버튼 클릭시 인트로씬으로 돌아가거나, 현재 씬을 다시 로드해야함.
    }
    
    // 게임 클리어시 호출
    // 4스테이지일때 보스방 클리어시 
    void Endding()
    {
        // 아웃트로 씬 로드해야함.
    }
}
