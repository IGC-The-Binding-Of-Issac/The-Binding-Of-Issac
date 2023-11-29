using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CutScene : MonoBehaviour
{
    [SerializeField] Image isaacSpot;
    [SerializeField] Image bossSpot;
    [SerializeField] Image isaacFont;
    [SerializeField] Image bossFont;
    [SerializeField] Image vs;
    [SerializeField] Image isaac;
    [SerializeField] Image bossImage;
    [SerializeField] RectTransform rectTransform;

    [Header("Sprites")]
    [SerializeField] Sprite[] bossSprite;
    [SerializeField] Sprite[] fontSprite;

    private void OnEnable() 
    {
        // ÄÆ¾À ÄÑÁú¶§
        if (gameObject.activeSelf)
        {
            bossImage.sprite = bossSprite[GameManager.instance.stageLevel - 1];
            bossFont.sprite = fontSprite[GameManager.instance.stageLevel - 1];

            isaacSpot.rectTransform.DOLocalMoveX(-250, 1f);
            isaac.rectTransform.DOShakePosition(4f, new Vector3(7f, 0, 0), 60);
            bossSpot.rectTransform.DOLocalMoveX(190, 1.2f);
            isaacFont.rectTransform.DOLocalMoveX(-240, 1.7f);
            bossFont.rectTransform.DOLocalMoveX(240, 2f);
            vs.rectTransform.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.FastBeyond360);
            vs.rectTransform.DOScale(Vector3.one, 2f);
        }
    }

    private void OnDisable()
    {
        // ÄÆ¾À ²¨Á³À»¶§ ÃÊ±âÈ­
        isaacSpot.rectTransform.DOLocalMoveX(-1094, 1f);
        bossSpot.rectTransform.DOLocalMoveX(2764, 1.2f);
        isaacFont.rectTransform.DOLocalMoveX(-1746, 1.7f);
        bossFont.rectTransform.DOLocalMoveX(2346, 2f);
        vs.rectTransform.DOScale(Vector3.zero, 0f);
    }
}
