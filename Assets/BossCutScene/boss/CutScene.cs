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
    [SerializeField] Sprite bossChgSprite;
    [SerializeField] Sprite Gurdy;
    [SerializeField] Sprite GurdyFont;
    [SerializeField] Sprite Larry;
    [SerializeField] Sprite LarryFont;
    [SerializeField] Sprite Gemini;
    [SerializeField] Sprite GeminiFont;
    [SerializeField] Sprite Monstro;
    [SerializeField] Sprite MonstroFont;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (gameObject.activeSelf)
        {
            if(GameManager.instance.stageLevel == 1)
            {
                bossImage.sprite = Gemini;
                bossFont.sprite = GeminiFont;
            }
            else if(GameManager.instance.stageLevel == 2)
            {
                bossImage.sprite = Monstro;
                bossFont.sprite = MonstroFont;
            }else if (GameManager.instance.stageLevel == 3)
            {
                bossImage.sprite = Larry;
                bossFont.sprite = LarryFont;
            }else if (GameManager.instance.stageLevel == 4)
            {
                bossImage.sprite = Gurdy;
                bossFont.sprite = GurdyFont;
            }
            isaacSpot.rectTransform.DOLocalMoveX(-250, 1f);
            isaac.rectTransform.DOShakePosition(4f, new Vector3(7f, 0, 0), 60);
            bossSpot.rectTransform.DOLocalMoveX(190, 1.2f);
            isaacFont.rectTransform.DOLocalMoveX(-240, 1.7f);
            bossFont.rectTransform.DOLocalMoveX(240, 2f);
            vs.rectTransform.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.FastBeyond360);
            vs.rectTransform.DOScale(Vector3.one, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
