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

    [Header("Sounds")]
    [SerializeField] AudioClip[] stageBGM;
    [SerializeField] AudioClip[] stageIntroBGM;

    [Header("Sound Object")]
    [SerializeField] AudioSource bgmObject;

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
}
