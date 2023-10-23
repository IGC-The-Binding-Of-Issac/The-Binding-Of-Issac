using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Image trinket; // Àå½Å±¸
    [SerializeField] Image active;  // ¾×Æ¼ºê
    [SerializeField] Text coinText; // ÄÚÀÎ
    [SerializeField] Text bombText; // ÆøÅº
    [SerializeField] Text keyText;  // ¿­¼è

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
        coinText.text = ItemManager.instance.coinCount.ToString();
        bombText.text = ItemManager.instance.bombCount.ToString();
        keyText.text = ItemManager.instance.keyCount.ToString();
    }
}
