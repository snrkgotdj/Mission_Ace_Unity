using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    public GameObject PopUpPrefab;


    UILabel                 _lbTitle;
    UIButtonScale           _btnScale;
    VertHoriScrollScript    _scroll;

    // Use this for initialization

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform childTrans = transform.GetChild(i);
            if ("Title" == childTrans.tag)
            {
                _lbTitle = childTrans.GetComponent<UILabel>();
                continue;
            }

            UILabel lbValue = childTrans.GetComponentInChildren<UILabel>();
            lbValue.text = Random.Range(1000, 10000).ToString();
        }

        _btnScale = GetComponent<UIButtonScale>() as UIButtonScale;
        _scroll = GetComponent<VertHoriScrollScript>();
    }

    public void SetTitle(string title)
    {
        _lbTitle.text = title;
    }

    private void Update()
    {
        if(_scroll.IsMove == true)
        {
            _btnScale.OnDisable();
            return;
        }
    }

    public void OnClick()
    {
        if(false == MainViewScript.Instance.GetComponent<UIScrollView>().enabled)
        {
            return;
        }

        GameObject inst = EventGroupMgr.Instance.PageEvent.gameObject.AddChild(PopUpPrefab);
        inst.GetComponentInChildren<UILabel>().text = _lbTitle.text;
    }
}
