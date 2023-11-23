using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dr_fetus : ItemInfo
{
    public GameObject attackBomb;
    public PlayerController ctr;

    public void Awake()
    {
        
    }
    public override void Start()
    {
        base.Start();
        ctr = GameManager.instance.playerObject.GetComponent<PlayerController>();
        SetItemCode(16);
        SetItemString("태아 박사",
                      "폭탄 발사",
                      "공격 시 폭탄을 발사한다."
                    + "\n공격속도 * 0.5");
    }
    public override void UseItem()
    {
        if (ItemManager.instance.PassiveItems[13])
        {
            ctr.knifePosition.gameObject.SetActive(false);
            ctr.knife.SetActive(false);
            ItemManager.instance.PassiveItems[13] = false;
            PlayerManager.instance.playerDamage -= 2.0f;
        }
        PlayerManager.instance.playerShotDelay /= 0.5f;
        base.UseItem();
    }
}
