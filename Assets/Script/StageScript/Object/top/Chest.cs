using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chest : MapObject
{
    [SerializeField] protected Sprite openSprite;
    [SerializeField] protected Sprite closeSprite;

    protected abstract override void initialization();                                      // 오브젝트 최초 생성시 초기화
    public override void ResetObject()                                                      // 오브젝트 초기화 ( 풀링 )
    {
        //초기화
        gameObject.layer = 15;
        gameObject.GetComponent<SpriteRenderer>().sprite = closeSprite;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }
    public abstract override void Returnobject();                                           // 오브젝트 초기화 ( 풀링 )

    protected virtual void OpenChest()                                                      // 상자 오픈
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;                      // 열린상자 이미지로 변경
        DropReward();
        OpenSound(); // 상자 

        gameObject.layer = 16; // 열린상자 스프라이트
    }
    protected virtual void OpenSound()                                                      // 상자 오픈 사운드
    {   
        gameObject.GetComponent<AudioSource>().Play();
    }                                                  

    protected virtual void DropReward() { }                                                 // 상자 보상
}
