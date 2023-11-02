using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ActiveInfo
{
    public Sprite redTearImg;

    private void Awake()
    {
        //0번 아이템, 6칸 필요함
        SetActiveItem(0, 6);
        SetActiveString("대못", 
            "일시적 악마 변신",
            "사용 시 캐릭터가 악마로 변하여 다음의 효과를 얻는다."
            + "\n 최대 체력 + 0.5칸"
            + "\n 이동 속도 - 0.08" 
            + "\n 공격력 + 0.5"
            + "\n 캐릭터의 눈물이 빨갛게 변한다.");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerMaxHp += 1;
        PlayerManager.instance.playerMoveSpeed -= 0.08f;
        PlayerManager.instance.playerDamage += 0.5f;

        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;
        Invoke("SetCanUse", 1f);
        }
    }
}
