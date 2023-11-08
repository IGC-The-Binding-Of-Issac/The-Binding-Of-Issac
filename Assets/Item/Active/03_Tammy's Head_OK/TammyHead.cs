using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TammyHead : ActiveInfo
{
    float beforeDamage;
    private GameObject activeTear;
    void Awake()
    {
        SetActiveItem(3, 1);
        SetActiveString("태미의 머리",
                        "충전식 눈물 폭발",
                        "사용 시 8방향으로 데미지가 25 더 높은 눈물을 동시에 발사한다.");
        beforeDamage = PlayerManager.instance.playerDamage;
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerDamage += 25f;
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, -1);
            activeTear = GameObject.Find("Tear(Clone)");
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public override void CheckedItem()
    {
        if (activeTear == null) PlayerManager.instance.playerDamage = beforeDamage;
    }
}
