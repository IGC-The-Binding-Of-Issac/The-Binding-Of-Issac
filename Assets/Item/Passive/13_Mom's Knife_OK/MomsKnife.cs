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
                    + "\n공격력 + 2"
                    + "\nDr.Fetus 보유 시 식칼이 생기지 않는다.");
    }
    public override void UseItem()
    {
        if (!ItemManager.instance.PassiveItems[16])
        {
        PlayerManager.instance.playerDamage += 2.0f;
        Invoke("GenerateKnife", 1.0f);
        }
    }

    private void GenerateKnife()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().knifePosition.gameObject.SetActive(true);
        GameManager.instance.playerObject.GetComponent<PlayerController>().knife.SetActive(true);
    }
}
