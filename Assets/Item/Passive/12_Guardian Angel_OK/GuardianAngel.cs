using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAngel : ItemInfo
{
    private void Start()
    {
        SetItemCode(12);
        SetItemString("수호 천사",
            "보호",
            "습득 시 캐릭터 주변에 천사 생성"
          + "\n공격 시 같이 눈물을 발사한다.");
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
