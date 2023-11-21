using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonMishap : ActiveInfo
{
    [SerializeField]
    private GameObject oops;

    public override void Start()
    {
        base.Start();
        SetActiveItem(18, 1);
        SetActiveString("레몬빛 사고",
                        "앗 ...",
                        "사용 시 캐릭터 주변에 노란 장판을 깐다."
                      + "\n장판에 닿는 적은 초당 24의 피해를 입는다.");
    }

    public override void UseActive()
    {
        if (canUse)
        {
            Transform currentPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
            GameObject flooring = Instantiate(oops, new Vector3(currentPosition.position.x, currentPosition.position.y - 1f, 0), Quaternion.identity);
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            
        }
    }
}
