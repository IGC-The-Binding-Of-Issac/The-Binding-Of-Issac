using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOTM : ItemInfo
{
    public GameObject tearPrf;
    public GameObject playerPrf;
    public Sprite redTearImg;
    public Sprite defaultTearImg;
    public Sprite MartyrImg;

    private void Start()
    {
        SetItemCode(0);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage++;
        //먹으면 캐릭터 위에 월계관 달림
        //playerPrf.transform.Find("HeadItem").GetComponent<SpriteRenderer>().sprite = MartyrImg; R키를 눌러야 적용됨.

        //눈물 색깔 빨간색으로 변경
        //tearPrf.gameObject.GetComponent<SpriteRenderer>().sprite = redTearImg; 게임 재시작해도 이미지가 초기화 안됨

        //눈물 터지는 애니메이션 변경
        
    }
}
