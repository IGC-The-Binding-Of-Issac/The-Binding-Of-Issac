using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TammyHead : ActiveInfo
{
    public void Awake()
    {
        base.Start();
        SetActiveItem(3, 1);
        SetActiveString("태미의 머리",
                        "충전식 눈물 폭발",
                        "사용 시 8방향으로 눈물을 동시에 발사한다.");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            StartCoroutine(TammyHeadAttack());
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }
    private IEnumerator TammyHeadAttack()
    {
        for(int i = 0; i < 2; i++) 
        { 
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, -1);
            yield return new WaitForSeconds(0.25f);
        }
    }

}
