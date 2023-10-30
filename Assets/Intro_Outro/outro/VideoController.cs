using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public float fadeDuration = 1f; // ���������� 100���� 0�� �Ǵµ� �ɸ��� �ð� (��)

    private Coroutine fadeCoroutine;

    void Start()
    {
        // �������� ������ ȣ��Ǵ� �̺�Ʈ�� �����մϴ�.
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // �������� ���߰� �̹����� ���������� 0���� ����ϴ�.
        vp.Stop();
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOut(rawImage, fadeDuration));
    }

    void Update()
    {
        // ����ڰ� ȭ���� Ŭ���ϸ� �������� ���߰� �̹����� ���������� ��� 0���� ����ϴ�.
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