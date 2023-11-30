using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ImageController : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;
    public Image image7;
    public Image image8;
    public Image image9;
    public Image image10;
    public GameObject SkipBtn;

    public float waitTime; // 이미지의 불투명도를 변경하기 시작하기까지의 대기 시간 (초)
    public float fadeDuration; // 이미지의 불투명도가 0이 되는데 걸리는 시간 (초)

    void Start()
    {
        waitTime = 8;
        fadeDuration = 10;

        // 20초 후에 이미지의 불투명도를 점차적으로 0으로 만드는 코루틴을 시작합니다.
        StartCoroutine(WaitAndFadeOut(image1, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image2, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image3, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image4, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image5, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image6, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image7, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image8, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image9, waitTime, fadeDuration));
        StartCoroutine(WaitAndFadeOut(image10, waitTime, fadeDuration));
        SkipBtn.GetComponent<RectTransform>().DOShakePosition(5,7,4);
        Invoke("ReturnIntro", waitTime + fadeDuration + 2f);
    }

    public void ImageInvisible()
    {
        image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 0);
        image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, 0);
        image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, 0);
        image4.color = new Color(image4.color.r, image4.color.g, image4.color.b, 0);
        image5.color = new Color(image5.color.r, image5.color.g, image5.color.b, 0);
        image6.color = new Color(image6.color.r, image6.color.g, image6.color.b, 0);
        image7.color = new Color(image7.color.r, image7.color.g, image7.color.b, 0);
        image8.color = new Color(image8.color.r, image8.color.g, image8.color.b, 0);
        image9.color = new Color(image9.color.r, image9.color.g, image9.color.b, 0);
        image10.color = new Color(image10.color.r, image10.color.g, image10.color.b, 0);
    }

    public void ImageVisible()
    {
        image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1);
        image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, 1);
        image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, 1);
        image4.color = new Color(image4.color.r, image4.color.g, image4.color.b, 1);
        image5.color = new Color(image5.color.r, image5.color.g, image5.color.b, 1);
        image6.color = new Color(image6.color.r, image6.color.g, image6.color.b, 1);
        image7.color = new Color(image7.color.r, image7.color.g, image7.color.b, 1);
        image8.color = new Color(image8.color.r, image8.color.g, image8.color.b, 1);
        image9.color = new Color(image9.color.r, image9.color.g, image9.color.b, 1);
        image10.color = new Color(image10.color.r, image10.color.g, image10.color.b, 1);
    }

    IEnumerator WaitAndFadeOut(Image targetImage, float waitTime, float duration)
    {
        yield return new WaitForSeconds(waitTime);

        float startTime = Time.time;
        Color startColor = targetImage.color;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            targetImage.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0, t));
            yield return null;
        }

        targetImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    void ReturnIntro()
    {
        SceneManager.LoadScene("01_Intro");
    }
}
