using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stuff : MonoBehaviour
{
    public GameObject stuffObject;
    public void SetStuff()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if(ItemManager.instance.ActiveItem != null)
        {
            GameObject obj = Instantiate(stuffObject) as GameObject;
            obj.GetComponent<Image>().sprite = ItemManager.instance.ActiveItem.GetComponent<SpriteRenderer>().sprite;
            obj.transform.SetParent(gameObject.transform);
        }

        if (ItemManager.instance.TrinketItem != null)
        {
            GameObject obj = Instantiate(stuffObject) as GameObject;
            obj.GetComponent<Image>().sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
            obj.transform.SetParent(gameObject.transform);
        }

        for(int i = 0; i < ItemManager.instance.PassiveItems.Length; i++)
        {
            if(ItemManager.instance.PassiveItems[i])
            {
                GameObject obj = Instantiate(stuffObject) as GameObject;
                obj.GetComponent<Image>().sprite = ItemManager.instance.itemTable.GetStuffImage(i);
                obj.transform.SetParent(gameObject.transform);
            }
        }
    }
}
