using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banner : MonoBehaviour
{
    public Vector2 BannerPosi;
    RectTransform rect;

    [SerializeField] GameObject[] stageTEXT;
    [SerializeField] Text itemTitle;
    [SerializeField] Text itemInfo;

    bool bannerState;
    void Start()
    {
        bannerState = true;
        rect = gameObject.GetComponent<RectTransform>();
    }
    
    public void ItemBanner(string Title, string info)
    {
        if(bannerState)
        {
            StageBannerControl(0); // stage Text 전부 끄기

            itemTitle.text = Title;
            itemInfo.text = info;

            StartCoroutine(CallBanner());
        }
    }
    public void StageBanner(int stage)
    {
        if(bannerState)
        {
            StageBannerControl(stage);
            itemTitle.text = "";
            itemInfo.text = "";
            StartCoroutine(CallBanner());
        }
    }

    void StageBannerControl(int stage)
    {
        for (int i = 0; i < stageTEXT.Length; i++)
        {
            if (i == stage - 1)
                stageTEXT[i].SetActive(true);
            else
                stageTEXT[i].SetActive(false);
        }
    }
    IEnumerator CallBanner()
    {
        if(bannerState)
        {
            bannerState = false;
            while (rect.anchoredPosition.x <= 0)
            {
                transform.Translate(Vector3.right * 7000f * Time.deltaTime);

                // 한 프레임을 기다립니다.
                yield return null;
            }

            rect.anchoredPosition = new Vector3(0,rect.anchoredPosition.y,0);
        
            yield return new WaitForSeconds(0.7f);

            while (rect.anchoredPosition.x <= 1000)
            {
                transform.Translate(Vector3.right * 7000f * Time.deltaTime);

                // 한 프레임을 기다립니다.
                yield return null;
            }

            rect.anchoredPosition = new Vector3(-1000, rect.anchoredPosition.y, 0);
            bannerState = true;
        }
    }
}
