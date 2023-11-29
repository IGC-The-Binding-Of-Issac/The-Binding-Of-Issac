using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CutScene : MonoBehaviour
{
    public Image isaacSpot;
    public Image bossSpot;
    public Image isaacFont;
    public Image bossFont;
    public Image vs;
    public Image isaac;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (gameObject.activeSelf)
        {
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
