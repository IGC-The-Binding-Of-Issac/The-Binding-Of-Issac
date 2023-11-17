using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplane : MonoBehaviour
{
    [SerializeField] Text itemInfo1th;
    [SerializeField] Text itemInfo2nd;
    [SerializeField] Collider2D[] col;
    public LayerMask itemlayer;
    private float radius;

    private Color[] colors = { Color.red, new Color(1, 0.6f, 0), Color.yellow, Color.green, Color.blue, new Color(0.1f, 0, 0.6f), new Color(0.4f, 0, 1)};
    void Start()
    {
        radius = 1.3f;
        StartCoroutine(title());
    }

    // Update is called once per frame
    void Update()
    {
        //RainbowTitle();
        if (GameManager.instance.playerObject != null)
        {
            col = Physics2D.OverlapCircleAll(GameManager.instance.playerObject.transform.position, radius, itemlayer);
            if (col.Length > 0)
            {
                if (col[0].GetComponent<ItemInfo>() != null)
                {
                    itemInfo1th.text = col[0].GetComponent<ItemInfo>().itemTitle;
                    itemInfo2nd.text = col[0].GetComponent<ItemInfo>().itemInformation;
                }

                else if (col[0].GetComponent<ActiveInfo>() != null)
                {
                    itemInfo1th.text = col[0].GetComponent<ActiveInfo>().itemTitle;
                    itemInfo2nd.text = col[0].GetComponent<ActiveInfo>().itemInformation;
                }

                else if (col[0].GetComponent<TrinketInfo>() != null)
                {
                    itemInfo1th.text = col[0].GetComponent<TrinketInfo>().itemTitle;
                    itemInfo2nd.text = col[0].GetComponent<TrinketInfo>().itemInformation;
                }

                else if(col[0].GetComponent<ShopTable>() != null)
                {
                    string[] info = col[0].GetComponent<ShopTable>().itemInfomation;
                    itemInfo1th.text = info[0];
                    itemInfo2nd.text = info[1];
                }
            }
            else
            {
                itemInfo1th.text = "";
                itemInfo2nd.text = "";
            }
        }
    }
    IEnumerator title()
    {
        int cnt = 0;
        while(true)
        {
            if(cnt > 6)
            {
                cnt = 0;
            }
            ChangeColor(colors[cnt]);
            cnt++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void ChangeColor(Color color)
    {
        itemInfo1th.color = color;
    }
}
