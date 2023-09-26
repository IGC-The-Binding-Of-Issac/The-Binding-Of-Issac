using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonMishap : ItemInfo
{
    LemonMishap()
    {
        itemCode = 36;
        itemName = "Lemon Mishap";
    }
    //사용 시 캐릭터가 서 있는 장소에 노란 장판을 설치한다.
    //장판을 밟는 적은 초당 10의 데미지를 입는다.
}
