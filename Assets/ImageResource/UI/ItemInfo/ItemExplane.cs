using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplane : MonoBehaviour
{
    public Text itemInfo1th;
    public Text itemInfo2nd;

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
            }
            else
            {
                itemInfo1th.text = "";
                itemInfo2nd.text = "";
            }
        }
    }
}
