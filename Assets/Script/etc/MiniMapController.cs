using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] int miniMapScale;
    [SerializeField] int miniMapCameraSize;

    [SerializeField] RectTransform miniMapUI;
    [SerializeField] Camera miniMapCamera;

    private void Start()
    {
        miniMapScale = 1;
        miniMapCameraSize = 30;
        AdjustmentMiniMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SizeChanger();
            AdjustmentMiniMap();
        }
    }


    void SizeChanger()
    {
        // 기본 사이즈 일때 ->  커짐
        if (miniMapScale == 1)
        {
            miniMapScale = 2;
            miniMapCameraSize = 50;
        }

        // 커진 사이즈 일때 -> 작아짐
        else if (miniMapScale == 2)
        {
            miniMapScale = 1;
            miniMapCameraSize = 30;
        }
    }

    void AdjustmentMiniMap()
    {
        miniMapUI.localScale = new Vector3(miniMapScale,miniMapScale,1);
        miniMapCamera.orthographicSize = miniMapCameraSize;
    }
}
