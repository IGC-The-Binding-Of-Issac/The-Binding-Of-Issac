using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Image trinket; // 장신구
    [SerializeField] Image active;  // 액티브
    [SerializeField] Text coinText; // 코인
    [SerializeField] Text bombText; // 폭탄
    [SerializeField] Text keyText;  // 열쇠

    [Header("Hearts")]
    [SerializeField] Transform heartUI; // 하트 UI 
    [SerializeField] GameObject emptyHeart; // 빈하트

    private void Start()
    {
        SetPlayerMaxHP(); // 최대체력 늘려야하는곳에서 호출해주기.
        SetPlayerCurrentHP(); // 현재 체력이 바뀌었을때 호출해주기 ( ex) 데미지 입었을때 )
    }

    private void Update()
    {
        UpdateUI();
        SetPlayerCurrentHP();
    }

    public void UpdateUI()
    {
        if (ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (ItemManager.instance.ActiveItem != null)
        {
            active.sprite = ItemManager.instance.ActiveItem.GetComponent<SpriteRenderer>().sprite;
        }
        coinText.text = ItemManager.instance.coinCount.ToString();
        bombText.text = ItemManager.instance.bombCount.ToString();
        keyText.text = ItemManager.instance.keyCount.ToString();
    }

    public void SetPlayerMaxHP()
    {
        for (int i = 0; i < PlayerManager.instance.playerMaxHp/2; i++)
        {
            GameObject eheart = Instantiate(emptyHeart) as GameObject;
            eheart.transform.SetParent(heartUI);
        }
    }

    public void SetPlayerCurrentHP()
    {
        int tmp = PlayerManager.instance.playerHp;
        for(int i = 0; i < heartUI.childCount; i++)
        {
            if(tmp >= 2)
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(2);
                tmp -= 2;
            }
            else if(tmp >= 1)
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(1);
                tmp -= 1;
            }
            else
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(0);
            }
        }
    }
}
