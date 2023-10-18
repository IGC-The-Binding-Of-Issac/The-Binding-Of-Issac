using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Image trinket; // Àå½Å±¸
    [SerializeField] Image active;  // ¾×Æ¼ºê
    [SerializeField] Text CoinText; // ÄÚÀÎ
    [SerializeField] Text BombText; // ÆøÅº
    [SerializeField] Text KeyText;  // ¿­¼è

    private void Update()
    {
        if(ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
