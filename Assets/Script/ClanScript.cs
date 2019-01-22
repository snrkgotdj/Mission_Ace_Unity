using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanScript : MonoBehaviour {

    public UILabel _lbTitle = null;
    public UILabel _lbTroophy = null;

    public void SetTitle(string title)
    {
        _lbTitle.text = title;
    }

    private void OnEnable()
    {
        if(null == _lbTitle || null == _lbTroophy)
        {
            Debug.LogError("클랜 이름이나 트로피 점수가 셋팅되지 않았습니다.");
            return;
        }

        _lbTroophy.text = Random.Range(0, 999).ToString();

        VertHoriScrollScript vertHori = GetComponent<VertHoriScrollScript>();
        if( null != vertHori)
        {
            vertHori.ScrollVertical = ClanMgr.Instance.GetComponent<UIScrollView>();
            vertHori.ScrollHorizontal = MainViewScript.Instance.UIScroll;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
