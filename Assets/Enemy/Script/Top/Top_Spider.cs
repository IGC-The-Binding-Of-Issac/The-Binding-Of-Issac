using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Top_Spider : Enemy
{
    //Fly �̵�
    [HideInInspector]
    protected float x;
    protected float y;
    protected float xRan;
    protected float yRan;
    protected float randRange; //�������� �̵��� ���� 
    protected float fTime; //�����̵� �ð�
    protected Transform justTrackingPlayerPosi; // ������ ���� ���� update������ player �ǽð� �޾ƿ���

    protected void Spider_Move_InitialIze()
    {
        // enemy �ʱ� ��ġ
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        xRan = x;
        yRan = y;
    }

    // �÷��̾� ����
    // 1. �����ȿ� ������ �� searching �� tracking -> fp : enemy���� �˻��� enemyPosi
    // 2. �׳� ������ tracking -> fp : �������� update���� �ִ� justTrackingPlayerPosi
    protected void Tracking(Transform fp)
    {
        if (fp == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, fp.transform.position, moveSpeed * Time.deltaTime);
    }

    //���� ������
    protected void Prwol()
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, moveSpeed * Time.deltaTime);
    }

    //���� ������ �˻�
    protected IEnumerator CheckPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);
            Debug.Log("checkPosi����");
            // x��ġ�� ���� ��ġ randRange���� , ������ġ -randRange����
            // y��ġ ����
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }

}