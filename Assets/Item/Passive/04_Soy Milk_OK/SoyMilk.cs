using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
    private void Start()
    {
        SetItemCode(4);
        SetItemString("����",
            "�� ������..",
            "���� �� ���ݷ� ���� * 0.2��" +
            "\n ���� �ӵ� * 5.5��");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerDamage *= 0.2f;
        PlayerManager.instance.playerShotDelay /= 5.5f;
        PlayerManager.instance.playerTearSize *= 0.4f;
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.CheckedShotDelay();
        //���� ũ�� 0.3��� ����
        //������ �ʿ��� ������ ���� ��� ����, ������ ���� �ߵ� (������ ��)
    }
}