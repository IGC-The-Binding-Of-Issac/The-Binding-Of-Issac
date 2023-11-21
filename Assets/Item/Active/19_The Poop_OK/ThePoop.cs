using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePoop : ActiveInfo
{
    [SerializeField]
    GameObject goldenPoop;

    public override void Start()
    {
        base.Start();
        SetActiveItem(19, 0);
        SetActiveString("황금똥",
                        "무한으로 즐겨요",
                        "사용 시 플레이어가 위치한 자리에 황금 똥을 싼다.");
    }

    public override void UseActive()
    {
        if (canUse)
        {
            Transform poopTransform = GameManager.instance.playerObject.GetComponent<Transform>();
            GameObject goldPoop = Instantiate(goldenPoop, new Vector3(poopTransform.position.x, poopTransform.position.y, 0), Quaternion.identity) as GameObject;

            goldPoop.transform.SetParent(GameManager.instance.roomGenerate.roomPool.GetChild(0).transform); // 그냥 아무방에다가 같이 묶어두기.
            SoundManager.instance.sfxObjects.Add(goldPoop.GetComponent<AudioSource>());
        }
    }
}
