using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAngel : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(12);
        SetItemString("수호 천사",
                      "보호",
                      "습득 시 캐릭터 주변에 천사 생성"
                    + "\nHoly Mantle 보유 시"
                    + "\n방 클리어 시 1회 보호막을 부여한다.");
    }

    public override void UseItem()
    {
        Invoke("GenerateAngel", 1.0f);
    }
    
    private void GenerateAngel()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().familiarPosition.gameObject.SetActive(true);
    }
}
