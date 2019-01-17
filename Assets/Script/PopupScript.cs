using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour {


    bool _isPopup = false;
    bool _isDistroy = false;

    private void Awake()
    {
        // 여기서 그거 뭐시냐 팝업 클릭이랑 다른데에서 클릭 안받게 해준다.

        // 먼저 Touch 해제하기
        MainViewScript.Instance.SetTouch(false);



    }

    void OnPress(bool bPress)
    {
        _isPopup = bPress;

    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && true == _isDistroy)
        {
            Destroy(gameObject);
            MainViewScript.Instance.SetNextTouchOn();
        }

        if (Input.GetMouseButtonDown(0) && false == _isPopup)
        {
            _isDistroy = true;
        }
    }
}
