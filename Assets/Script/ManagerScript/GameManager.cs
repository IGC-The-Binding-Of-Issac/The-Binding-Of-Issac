using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public Transform miniMapPosition;

    [Header("Unity Setup")]
    public StageGenerate stageGenerate;
    public RoomGenerate roomGenerate;
    public GameObject myCamera;
    public GameObject miniMapCamera;



    [Header("reload")]
    [SerializeField] private float curTime;

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
#if TEST_MODE
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("02_Game");
        }
#endif
        // 스테이지 재생성
        // R키 누르기 -> 스테이지 시작시로 변경 할것. 
        if (Input.GetKey(KeyCode.R) && !UIManager.instance.LodingImage.activeSelf) 
        {
            curTime += Time.deltaTime;

            if (curTime >= 2f) // 2초간 누르고있으면
                SceneManager.LoadScene("02_Game"); // 1스테이지로 돌아가서 재시작합니당
            
            //UIManager.instance.OnLoading();
            //StageStart();
        }

        if(Input.GetKeyUp(KeyCode.R) && curTime <= 2.4f) 
        {
            curTime = 0;
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
        miniMapCamera.transform.SetParent(myCamera.transform); // 카메라의 위치를 초기화
        miniMapCamera.GetComponent<MiniMapController>().initCamera();
        // ** 이부분이 없으면 스테이지 생성될때 스테이지내 방들이 삭제되면서 카메라도 같이 사라집니다 **


        // int cnt = 25; // 방 생성 실패 한계치
        // 방생성시 오류발생시
        // while cnt 로 횟수제한 줘야함! 
        while (true) 
        {
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom))
            {
                roomGenerate.ClearRoom(); // 현재 생성되어있는 방 / 오브젝트 / 몬스터 등등 전부 초기화
                SoundManager.instance.sfxDestoryObjects = new List<AudioSource>(); // soundManager의 sfxDestoryObjects 초기화.
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
