using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOTM : ItemInfo
{
    [Header("Sprite")]
    public Sprite redTearImg;
    public Sprite MartyrImg;


    public override void Start()
    {
        base.Start();
        SetItemCode(0);
        SetItemString("월계관",
                      "순교자의 힘",
                      "습득 시 공격력 + 1" +
                      "\n눈물 색깔을 빨간색으로 바꾼다.");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage++;
        PlayerManager.instance.CheckedDamage();
        //Dr.Fetus 제외 나머지 눈물 빨간색으로 변경
        if (!ItemManager.instance.PassiveItems[16])
        {
            PlayerManager.instance.SetTearSkin(1);
        }
        Invoke("getBOTM", 1f);
    }

    public void getBOTM()
    {
        // 캐릭터 위 월계관 생성
        GameManager.instance.playerObject.GetComponent<PlayerController>().HeadItem.GetComponent<SpriteRenderer>().sprite = MartyrImg;
    }
}
