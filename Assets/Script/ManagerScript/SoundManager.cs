using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region singleton
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Audio Cilps")]
    [SerializeField] AudioClip[] stageBGM;
    [SerializeField] AudioClip[] stageIntroBGM;

    [Header("Sound Object")]
    public AudioSource bgmObject; // BGM 사운드 오브젝트
    public AudioSource playerObject;  // 플레이어 사운드 오브젝트
    public AudioSource[] enemyObject; // 몬스터 사운드 오브젝트 
    public AudioSource[] stageObject; // 맵 오브젝트 사운드 오브젝트 
    // 이펙트 사운드는 해당 오브젝트 생성할때 여기서 값을 가져가서 사용해줍시다.

    [Header("Sound State")]
    [SerializeField] private float volumeBGM; // 배경 사운드
    [SerializeField] private float volumePlayer; // 플레이어 관련 사운드
    [SerializeField] private float volumeEffect; // 총알 그외 이펙트 등등
    [SerializeField] private float volumeObject; // 맵 오브젝트 사운드
    [SerializeField] private float volumeEnemy; // 적 사운드

    private void Start()
    {
        SoundInit();
        volumeBGM = bgmObject.volume; // 배경 사운드 수치 받
    }

    void SoundInit()
    {
        volumeBGM = 0.5f;
        volumePlayer = 0.5f;
        volumeEffect = 0.5f;
        volumeObject = 0.5f;
        volumeEnemy = 0.5f;
    }

    public void OnStageIntroBGM()
    {
        // 재생되고있는 bgm이 있으면  bgm 정지.
        if (bgmObject.isPlaying)
            bgmObject.Stop();

        // 현재 bgm을 현재 스테이지의 bgm으로 변경
        bgmObject.clip = stageBGM[GameManager.instance.stageLevel - 1];

        // intro bgm은 반복하지않음
        bgmObject.loop = true;

        // intro bgm 실행
        bgmObject.Play();
    }

    public void PlayerHitSound()
    {
       
    }
    public void PlayerDeadSound()
    {

    }

    public void VolumeControl()
    {
        // 배경 사운드 ( SoundManager BGM 출력하는 오브젝트 관리)
        // 배경

        // 오브젝트 사운드  ( 해당 오브젝트들이 생성될때 오브젝트들을 받아줘야할듯 )
        // 돌, 똥, 불 터지는 소리
        // 문 열리는 소리 ( 클리어 했을때 )
        // 상자 열리는 소리 

        // 플레이어 사운드 ( GameManager 의 playerobject가 플레이어가 출력하는 사운드 오브젝트 관리.
        // 사망시 
        // 피격시
        // 동전/열쇠/폭탄 먹는 소리
        // 아이템 획득|교체 사운드

        // 이펙트 사운드
        // 폭탄 터지는 소리 ( 설치될때 사운드 매니저에서 볼륨수치 가져와서 수정해주기 )
        // 눈물 터지는 소리 
        // 상점/황금방 문 여는 소리 ( 사용할떄 가져와서 수정해줍시다. )

        // 몬스터 사운드 ( 각 방에 enemis 변수 활용 ㄱ )
        // 기본 사운드
        // 사망 사운드
    }
}
