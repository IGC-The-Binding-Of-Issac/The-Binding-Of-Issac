using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);

    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.42f;
        PlayerManager.instance.SetHeadSkin(2);
        //�̸��� ���� �� �� �� �����.
        //ĳ������ ��� ������ ������ �߻��Ѵ�. [4��]
    }
}