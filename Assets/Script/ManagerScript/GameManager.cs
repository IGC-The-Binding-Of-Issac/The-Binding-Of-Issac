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
        roomGenerate.SetObjectPooling(); // set room object pool

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

        // 현재 플레이어 오브젝트가 없을때.
        if (playerObject == null)
        {
            GameObject obj = Instantiate(roomGenerate.objectPrefabs[9]) as GameObject; // 플레이어를 생성
            playerObject = obj; // playerObject 초기화
            
            // SoundManager의 플레이어 관련 사운드 오브젝트 초기화
            SoundManager.instance.playerObject = playerObject.GetComponent<AudioSource>(); 
        }

        myCamera.transform.SetParent(null); // 카메라의 위치를 초기화
        // ** 이부분이 없으면 스테이지 생성될때 스테이지가 초기화되면서 카메라도 같이 사라집니다 ** 


        int cnt = 15; // 방 생성 실패 한계치
        while (cnt-- > 0) 
        {
            // 스테이지 구조 생성 시도
            // 정상적으로 구조가 생성되면 true
            // 실패하면 false 리턴
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom)) 
            {
                roomGenerate.ClearRoom(); // 현재 생성되어있는 방 / 오브젝트 / 몬스터 등등 전부 초기화
                SoundManager.instance.sfxObjects = new List<AudioSource>(); // soundManager의 SFXObjects 초기화.
                roomGenerate.CreateRoom(stageLevel, stageSize); // 방 생성
                myCamera.transform.position = playerObject.transform.position;

                SoundManager.instance.OnStageBGM();
                SoundManager.instance.SFXInit();
                StartCoroutine(UIManager.instance.StageBanner(stageLevel));
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
