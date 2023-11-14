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
        SetItemString("월계관",
                      "순교자의 힘",
                      "습득 시 공격력 + 1" +
                      "\n눈물 색깔을 빨간색으로 바꾼다.");
    }
    public override void UseItem()
    {
        //플레이어 데미지 + 1
        PlayerManager.instance.playerDamage++;

        //눈물 색깔 빨간색으로 변경
        if (ItemManager.instance.PassiveItems[16] == false)
            PlayerManager.instance.SetTearSkin(1);

        //월계관 달리는 함수 1초뒤 실행
        Invoke("getBOTM", 1f);

        //눈물 터지는 애니메이션 변경

    }

    public void getBOTM()
    {
        //먹으면 캐릭터 위에 월계관 달림
        GameManager.instance.playerObject.GetComponent<PlayerController>().HeadItem.GetComponent<SpriteRenderer>().sprite = MartyrImg;
    }
}
