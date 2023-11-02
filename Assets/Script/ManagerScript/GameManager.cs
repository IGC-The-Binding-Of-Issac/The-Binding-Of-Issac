using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int maxStage;

    [Header("Unity Setup")]
    public StageGenerate stageGenerate;
    public RoomGenerate roomGenerate;
    public GameObject myCamera;

    private void Start()
    {
        SetStage(1); // 1stage create
        roomGenerate.SetPrefabs(); // room Prefabs Setting

        // 게임 시작 ( 스테이지 생성 )
        Invoke("StageStart", 0.3f);
    }

    void Update()
    {
        // 스테이지 생성 테스트.
        // R키 누르기 -> 스테이지 시작시로 변경 할것. 
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            UIManager.instance.OnLoading();
            StageStart();
            
        }
    }
    public void StageStart()
    {
        // Create stage/room

        if (playerObject == null)
        {
            GameObject obj = Instantiate(roomGenerate.objectPrefabs[9]) as GameObject;
            playerObject = obj;
        }

        myCamera.transform.SetParent(null);

        int cnt = 15;
        while (cnt-- > 0)
        {
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom))
            {
                roomGenerate.ClearRoom(); // room reset -> create
                roomGenerate.CreateRoom(stageLevel, stageSize); // room create
                myCamera.transform.position = playerObject.transform.position;

                SoundManager.instance.OnStageIntroBGM();
                break;
            }
        }
    }

    public void NextStage()
    {
        SetStage(++stageLevel); // 스테이지 세팅
        UIManager.instance.OnLoading();
        StageStart(); // 스테이지 생성
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
}
