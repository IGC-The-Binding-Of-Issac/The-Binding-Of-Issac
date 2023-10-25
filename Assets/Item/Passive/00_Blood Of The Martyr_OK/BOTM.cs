using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOTM : ItemInfo
{
    public Sprite redTearImg;
    public Sprite MartyrImg;


    private void Start()
    {
        SetItemCode(0);

    }
    public override void UseItem()
    {
        //플레이어 데미지 + 1
        PlayerManager.instance.playerDamage++;

        //눈물 색깔 빨간색으로 변경
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;

        //월계관 달리는 함수 1초뒤 실행
        Invoke("getBOTM", 1f);

        //눈물 터지는 애니메이션 변경

    }

    public void getBOTM()
    {
        //먹으면 캐릭터 위에 월계관 달림
        GameManager.instance.playerObject.transform.Find("HeadItem").GetComponent<SpriteRenderer>().sprite = MartyrImg;
    }
}
