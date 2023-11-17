using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplane : MonoBehaviour
{
    [SerializeField] Text itemInfo1th;
    [SerializeField] Text itemInfo2nd;

    private float radius;

    [SerializeField] Collider2D[] col;
    public LayerMask itemlayer;
    void Start()
    {
        radius = 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.playerObject != null)
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
            //StartCoroutine(RainbowTitle());
        }
    }
    IEnumerator RainbowTitle()
    {
        int cnt = 8;
        while(cnt > 0) 
        {
            if(cnt == 1)
            {
                itemInfo1th.color = Color.red;
            }
            else if(cnt == 2)
            {
                itemInfo1th.color = new Color(1, 0.5f, 1);
            }
            else if (cnt == 3)
            {
                itemInfo1th.color = Color.yellow;
            }
            else if (cnt == 4)
            {
                itemInfo1th.color = Color.green;
            }
            else if (cnt == 5)
            {
                itemInfo1th.color = Color.blue;
            }
            else if (cnt == 6)
            {
                itemInfo1th.color = new Color(0.1f, 0, 0.5f);
            }
            else if (cnt == 7)
            {
                itemInfo1th.color = new Color(0.7f, 0, 1);
            }
            cnt--;
            yield return new WaitForSeconds(3f);
        }
    }
}
