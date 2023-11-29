using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] float miniMapScale;
    [SerializeField] int miniMapCameraSize;

    [SerializeField] Transform miniMapLockTransform;

    [Header("Unity Setup")]
    [SerializeField] RectTransform miniMapUI;
    [SerializeField] Camera miniMapCamera;
    [SerializeField] Transform mainCameraTransfrom;

    private void Start()
    {
        initCamera();
    }

    public void initCamera()
    {
        miniMapScale = 1;
        miniMapCameraSize = 30;
        AdjustmentMiniMap();
        MiniMapLock(1);
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
        if (miniMapScale <= 1)
        {
            miniMapScale = 2.5f;
            miniMapCameraSize = 70;
            MiniMapLock(2);
        }

        // 커진 사이즈 일때 -> 작아짐
        else if (miniMapScale >= 2)
        {
            miniMapScale = 1;
            miniMapCameraSize = 30;
            MiniMapLock(1);
        }
    }
    void AdjustmentMiniMap()
    {
        miniMapUI.localScale = new Vector3(miniMapScale,miniMapScale,1);
        miniMapCamera.orthographicSize = miniMapCameraSize;
    }

    void MiniMapLock(int mode)
    {
        switch(mode)
        {
            // 작아졌을때
            case 1:
                miniMapCamera.transform.SetParent(mainCameraTransfrom);
                miniMapCamera.transform.localPosition = Vector3.zero;
                break;

            // 커졌을때
            case 2:
                if (miniMapLockTransform == null && GameManager.instance.miniMapPosition != null)
                {
                    miniMapLockTransform = GameManager.instance.miniMapPosition;
                }
                miniMapCamera.transform.SetParent(miniMapLockTransform);
                miniMapCamera.transform.localPosition = Vector3.zero;
                break;
        }
    }
}
