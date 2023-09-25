using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite redHeart;
    public Sprite halfRedHeart;

    public Sprite soulHeart;
    public Sprite halfSoulHeart;

    public Sprite blackHeart;
    public Sprite halfBlackHeart;

    public Sprite eternalHeart;

    void Update()
    {
        HpSystem();
    }

    void HpSystem()
    {
        //현재 체력
        int hp = GameController.instance.playerHp;
        //최대 체력
        int maxHp = GameController.instance.playerMaxHp;
        for (int i = 0; i < hearts.Length; i++)
        {
            //현재 체력이 최대 체력을 넘지 않게 함
            if (hp > maxHp)
            {
                hp = maxHp;
            }
            //현재 체력만큼 체력 sprite 활성화
            if (i < hp)
            {
                if (i % 2 == 1)
                {
                    hearts[i].sprite = redHeart;
                }
                else
                {
                    hearts[hp-1].sprite = halfRedHeart;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            //최대 체력만큼 빈 체력 활성화
            if (i < maxHp)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
}