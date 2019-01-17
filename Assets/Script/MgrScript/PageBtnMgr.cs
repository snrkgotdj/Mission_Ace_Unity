using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageBtnMgr : MonoBehaviour {

    public int SelectSizeX, SelectSizeY;
    public int StandSizeX, StandSizeY;
    public int SelectPad;
    public int PosY;

    private UICenterOnChild _grid = null;
    private Transform _trans = null;
    private GameObject _selectPage = null;

    // Use this for initialization
    private void Start () 
    {
        _grid = MainViewScript.Instance.GetComponentInChildren<UICenterOnChild>() as UICenterOnChild;
        _trans = GetComponent<Transform>();
        SetCenterBtn(_trans.GetChild(2));
    }

    private void Update()
    {
        if (_selectPage == _grid.centeredObject)
            return;

        _selectPage = _grid.centeredObject;

        for(int i = 0; i < _trans.childCount; ++i)
        {
            Transform btnTrans = _trans.GetChild(i);
            if (btnTrans.tag == _selectPage.tag)
            {
                SetCenterBtn(btnTrans);
            }
        }
    }


    // 선택된 버튼을 하이라이트로 바꾸는 코드
    public void SetCenterBtn(Transform _centerBtn)
    {
        int startPosX = (int)(-Screen.width * 0.5);
        Vector3 pos = new Vector3(startPosX, PosY);

        for (int i = 0; i < _trans.childCount; ++i)
        {
            Transform obj = _trans.GetChild(i);

            if (_centerBtn == obj) // 현재버튼이 선택된 버튼일때
            {
                if (0 != i) // 처음 버튼이 아닐때 더해준다
                    pos.x += StandSizeX;

                pos.x += SelectPad;

                obj.localPosition = pos;
                obj.GetComponent<UISprite>().SetDimensions(SelectSizeX, SelectSizeY);

                pos.x += SelectPad + SelectSizeX - StandSizeX;
            }

            else
            {
                if(0 != i) // 처음 버튼이 아닐때 더해준다
                    pos.x += StandSizeX;

                obj.localPosition = pos;
                obj.GetComponent<UISprite>().SetDimensions(StandSizeX, StandSizeY);
            }
            
        }
    }

    static private PageBtnMgr inst = null;

    public static PageBtnMgr Instance
    {
        get
        {
            if(null == inst)
            {
                inst = FindObjectOfType(typeof(PageBtnMgr)) as PageBtnMgr;
                if(null == inst)
                {
                    Debug.LogError("There is no PageBtnMgr");
                }
            }
            return inst;
        }
    }
}
