using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsKnife : ItemInfo
{
    public Sprite knifeImg;
    private void Start()
    {
        SetItemCode(13);
    }
    public override void UseItem()
    {
        //������ ������� ���� ��� Į�� �θ޶�ó�� ������.
        PlayerManager.instance.playerDamage *= 4f;
        PlayerManager.instance.playerShotDelay *= 2.5f;
        PlayerManager.instance.CheckedShotDelay();

        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = knifeImg;
    }
}
