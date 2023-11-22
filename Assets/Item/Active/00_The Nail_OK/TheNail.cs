using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ActiveInfo
{

    public void Awake()
    {
        base.Start();
        SetActiveItem(0, 6);
        SetActiveString("대못",
                        "일시적 악마 변신",
                        "사용 시 캐릭터가 악마로 변하여 다음의 효과를 얻는다."
                        + "\n최대 체력 + 1칸"
                        + "\n이동 속도 - 0.08"
                        + "\n공격력 + 0.5"
                        + "\n캐릭터의 눈물이 빨갛게 변한다.");
        Invoke("SetCanChangeItem", 1f);
        Debug.Log(gameObject.GetComponent<ActiveInfo>().currentEnergy);
    }
    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerMaxHp += 2;
            UIManager.instance.AddHeart();
            PlayerManager.instance.playerMoveSpeed -= 0.08f;
            PlayerManager.instance.playerDamage += 0.5f;
            PlayerManager.instance.SetHeadSkin(4);
            PlayerManager.instance.SetBodySkin(2);
            PlayerManager.instance.SetTearSkin(1);

            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            base.UseActive();
            Invoke("SetCanChangeItem", 1f);
            Invoke("SetCanUse", 1f);
        }
    }
}
