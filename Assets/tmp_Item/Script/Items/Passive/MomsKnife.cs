using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsKnife : ItemInfo
{
    MomsKnife()
    {
        itemCode = 14;
        itemName = "Mom's Knife";
    }
    // 획득 시 캐릭터가 식칼을 항상 소지하게 됨.
    // 공격 시 소지한 식칼을 공격 방향으로 발사 → 돌아온다. 부메랑
    // 식칼은 [playerDamage x 4]의 피해를 입힌다.
    // 날아가는 속도와 거리는 눈물 속도와 사거리에 비례한다.
}
