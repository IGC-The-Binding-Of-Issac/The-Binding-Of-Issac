using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public float fadeDuration = 1f; // 불투명도가 100에서 0이 되는데 걸리는 시간 (초)

    private Coroutine fadeCoroutine;

    void Start()
    {
        // 동영상이 끝나면 호출되는 이벤트를 설정합니다.
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // 동영상을 멈추고 이미지의 불투명도를 0으로 만듭니다.
        vp.Stop();
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOut(rawImage, fadeDuration));
    }

    void Update()
    {
        // 사용자가 화면을 클릭하면 동영상을 멈추고 이미지의 불투명도를 즉시 0으로 만듭니다.
        if (Input.GetMouseButtonDown(0))
        {
            videoPlayer.Stop();
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
        }
    }

    IEnumerator FadeOut(RawImage image, float duration)
    {
        float startTime = Time.time;
        Color startColor = image.color;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            image.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0, t));
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }
}