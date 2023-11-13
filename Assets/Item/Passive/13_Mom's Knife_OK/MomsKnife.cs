using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsKnife : ItemInfo
{
    public Sprite knifeImg;
    private void Start()
    {
        SetItemCode(13);
        SetItemString("엄마의 식칼",
                      "저는 죽이지 않았습니다.",
                      "습득 시 눈물이 사라지고 식칼을 날린다."
                    + "\n공격력 * 4"
                    + "\n공격속도 / 4");
    }
    public override void UseItem()
    {
        //눈물이 사라지고 눈물 대신 칼이 부메랑처럼 나간다.
        PlayerManager.instance.playerDamage *= 4f;
        PlayerManager.instance.playerShotDelay *= 2.5f;
        PlayerManager.instance.CheckedShotDelay();

        if (!ItemManager.instance.PassiveItems[16])
        {
            //Debug.Log("응애");
        //PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = knifeImg;
        }
    }
}
