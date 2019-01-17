using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSelectButton : MonoBehaviour {

    public Transform Page = null;

    Transform _trans = null;


    private void Start()
    {
        if (null == Page)
            Debug.LogError("페이지를 셋팅하세요");

        _trans = GetComponent<Transform>();
        gameObject.tag = Page.tag;

        UIButton btn = GetComponent<UIButton>();
        EventDelegate del = new EventDelegate(this, "OnClickMe");
        EventDelegate.Set(btn.onClick, del);
    }

    private void OnClickMe()
    {
        if (null == Page)
            return;

        MainViewScript.Instance.UiCenter.CenterOn(Page);
        PageBtnMgr.Instance.SetCenterBtn(_trans);
    }

}
