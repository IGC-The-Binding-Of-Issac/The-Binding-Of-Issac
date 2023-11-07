using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Banner : MonoBehaviour
{
    public Vector2 BannerPosi;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //필요한데서 갖다쓰면 되는 함수 => StartCoroutine(CallBanner());
    }
    IEnumerator CallBanner()
    {
        while (rect.anchoredPosition.x <= 0)
        {
            transform.Translate(Vector3.right * 7000f * Time.deltaTime);

            // 한 프레임을 기다립니다.
            yield return null;
        }

        rect.anchoredPosition = new Vector3(0,rect.anchoredPosition.y,0);
        
        yield return new WaitForSeconds(2f);

        while (rect.anchoredPosition.x <= 1000)
        {
            transform.Translate(Vector3.right * 7000f * Time.deltaTime);

            // 한 프레임을 기다립니다.
            yield return null;
        }

        rect.anchoredPosition = new Vector3(-1000, rect.anchoredPosition.y, 0);
    }
}
