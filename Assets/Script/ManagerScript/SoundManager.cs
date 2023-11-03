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
    public List<AudioSource> enemyObject; // 몬스터 사운드 오브젝트 
    public List<AudioSource> stageObject; // 맵 오브젝트 사운드 오브젝트 
    // 이펙트 사운드는 해당 오브젝트 생성할때 여기서 값을 가져가서 사용해줍시다.

    [Header("Sound State")]
    [SerializeField] private float volumeMaster; // 사운드
    [SerializeField] private float volumeBGM; // 사운드
    [SerializeField] private float volumeSFX; // 사운드

    private void Start()
    {
        SoundInit();

        bgmObject.volume = volumeBGM;
    }

    void SoundInit()
    {
        volumeMaster = 1.0f;
        volumeBGM = 0.5f;
        volumeSFX = 0.5f;

        enemyObject = new List<AudioSource>();
        stageObject = new List<AudioSource>();
    }

    public void OnStageBGM()
    {
        // 재생되고있는 bgm이 있으면  bgm 정지.
        if (bgmObject.isPlaying)
            bgmObject.Stop();

        // 현재 bgm을 현재 스테이지의 bgm으로 변경
        bgmObject.clip = stageBGM[GameManager.instance.stageLevel - 1];

        // bgm 반복
        bgmObject.loop = true;

        // bgm 실행
        bgmObject.Play();
    }
    /*
Master Volume 
  -
Music Volume 
  - BGM
SFX Volume
  - 플레이어 피격 사운드
  - 플레이어 사망 사운드
  - 동전/열쇠/폭탄 획득 사운드
  - 아이템 획득/교체 사운드
  - 플레이어 눈물 터지는 소리
  - 폭탄 터지는 소리
  - 돌,똥,불 터지는 사운드
  - 방 클리어시 문열릴때 사운드
  - 상자 오픈시 사운드
  - 상점/황금방 문 여는 소리
  - 몬스터 기본 사운드
  - 몬스터 사망 사운드
*/
}
