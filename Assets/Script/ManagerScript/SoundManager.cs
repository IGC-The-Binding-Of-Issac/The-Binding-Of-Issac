using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

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
    [SerializeField] AudioClip[] stageBGM;          // 스테이지 배경 clip
    [SerializeField] AudioClip[] bossRoomBGM;       // 보스방   배경 clip
    [SerializeField] AudioClip[] doorClip;          // 문            clip
    [SerializeField] AudioClip[] enemyDeadClip;     // 몬스터   사망 clip

    [Header("Sound Object")]
    [SerializeField]
    public AudioSource bgmObject; // BGM 사운드 오브젝트
    [SerializeField]
    public AudioSource playerObject;  // 플레이어 사운드 오브젝트
    [SerializeField]
    public List<AudioSource> sfxObjects = new List<AudioSource>(); // SFX 사운드 오브젝트트 ( 풀링 적용된 오브젝트
    [SerializeField]
    public List<AudioSource> sfxDestoryObjects = new List<AudioSource>(); // SFX 사운드 오브젝트트 ( 풀링 적용X 오브젝트

    [Header("Sound State")]
    [SerializeField] private int[] volumes;
    // [0] master   [1] BGM   [2] SFX
    private void Start()
    {
        SoundInit();
        BGMInit();
    }


    #region volume Control
    public int[] GetVolumes()
    {
        return volumes;
    }

    public float GetSFXVolume()
    {
        return (volumes[2] / 9.0f) * (volumes[0] / 9.0f);
    }

    public int VolumeControl(int mode, int increase)
    {
        // mode 0 : master  1 : bgm  2: sfx
        switch(increase)
        {
            // 사운드 증가
            case 1:
                if (volumes[mode] < 9)
                {
                    volumes[mode]++;
                    ObjectVolumeControl(mode);
                }
                return volumes[mode];

            // 사운드 감소
            case 2:
                if (volumes[mode] > 0)
                {
                    volumes[mode]--;
                    ObjectVolumeControl(mode);
                }
                return volumes[mode];
        }
        return 0;
    }
    void ObjectVolumeControl(int mode)
    {
        switch(mode)
        {
            // master volume
            case 0:
                BGMInit();
                SFXInit();
                break;

            // bgm volume
            case 1:
                BGMInit();
                break;

            // sfx volume
            case 2:
                SFXInit();
                break;

        }
    }
    void BGMInit()
    {
        bgmObject.volume = (volumes[1] / 9.0f) * (volumes[0] / 9.0f);
    }

    public void SFXInit()
    {
        for(int i = 0; i < sfxObjects.Count; i++)
        {
            if(sfxObjects[i] != null)
            {
                sfxObjects[i].volume = (volumes[2] / 9.0f) * (volumes[0] / 9.0f);
            }
        }

        for(int i = 0; i < sfxDestoryObjects.Count; i++)
        {
            if (sfxDestoryObjects[i] != null)
            {
                sfxDestoryObjects[i].volume = (volumes[2] / 9.0f) * (volumes[0] / 9.0f);
            }
        }

        playerObject.volume = (volumes[2] / 9.0f) * (volumes[0] / 9.0f);
    }

    void SoundInit()
    {
        volumes = new int[3];
        volumes[0] = 5;
        volumes[1] = 2;
        volumes[2] = 3;
    }
    #endregion

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

    public void OnBossBGM(int clearBoss)
    {
        //clearBoss : 0 -> 보스방 입장
        //clearBoss : 1 -> 보스방 퇴장
        if (bgmObject.isPlaying)
            bgmObject.Stop();

        bgmObject.clip = bossRoomBGM[clearBoss];
        if(clearBoss == 1)
          bgmObject.loop = false;
        
        bgmObject.Play();
    }
    
    public void aft(int clear)
    {
        StartCoroutine(AfterBossBGM(clear));
    }

    public IEnumerator AfterBossBGM(int clearBoss)
    {
        OnBossBGM(1);
        yield return new WaitForSeconds(7f);
        OnStageBGM();
    }
    public AudioClip GetDoorClip(int mode)
    {
        switch(mode)
        {
            case 0: // close
                return doorClip[0];
            case 1: // open
                return doorClip[1];
            case 2: // using key
                return doorClip[2];
        }
        return null;
    }

    public AudioClip GetEnemyDeadClip()
    {
        int rd = Random.Range(0,enemyDeadClip.Length);

        if (enemyDeadClip[rd] == null)
            return enemyDeadClip[0];

        return enemyDeadClip[rd];
    }
}
