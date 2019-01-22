using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainViewScript : MonoBehaviour {

    public UICenterOnChild UiCenter { get; private set; }
    public UIScrollView UIScroll { get; private set; }

    Transform _trans = null;

    bool _isNextTouch = false;

    // Use this for initialization

    public void SetTouch(bool bTouch)
    {
        UIScroll.enabled = bTouch;

        //이벤트 매니저의 터치 종료
        EventGroupMgr.Instance.SetTouch(bTouch); // 이벤트들의 터치 종료
        EventGroupMgr.Instance.GetComponent<UIScrollView>().enabled = bTouch;// 세로 스크롤 터치종료

        //덱 매니저의 터치 종료
        DackMgr.Instance.SetTouch(bTouch); // 카드들의 터치 종료
        DackMgr.Instance.GetComponent<UIScrollView>().enabled = bTouch; // 세로 스크롤 터치종료
    }

    public void SetNextTouchOn()
    {
        _isNextTouch = true;
    }

    private void Awake()
    {
        UiCenter = GetComponentInChildren<UICenterOnChild>();
        UIScroll = GetComponent<UIScrollView>();

        _trans = GetComponent<Transform>();
    }

    private void Update()
    {
        if(_isNextTouch)
        {
            _isNextTouch = false;
            SetTouch(true);
        }
    }

    private static MainViewScript _inst = null;
    public static MainViewScript Instance
    {
        get
        {
            if (null == _inst)
            {
                _inst = FindObjectOfType(typeof(MainViewScript)) as MainViewScript;
                if (null == _inst)
                {
                    Debug.LogError("There's no MainView object");
                }
            }
            return _inst;
        }
    }
}
