using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanMgr : MonoBehaviour {

    public GameObject ClanPrefab;
    public UIScrollView UIScroll { get; private set; }

    public int FirstClanCount;
    public int Padding;
    public int FirstStartY;
	

	void Start () 
    {
        Vector3 pos = new Vector3(0, FirstStartY);

        int clanSizeY = ClanPrefab.GetComponent<UISprite>().height;

		for(int i = 0; i < FirstClanCount; ++i)
        {
            GameObject clan = gameObject.AddChild(ClanPrefab);
            clan.transform.localPosition = pos;

            pos.y -= Padding + clanSizeY;

            clan.GetComponent<ClanScript>().SetTitle("Clan_" + i.ToString());
        }

        UIScroll = GetComponent<UIScrollView>();

        VertHoriScrollScript vertHori = ClanPrefab.GetComponent<VertHoriScrollScript>();
        vertHori.ScrollVertical = UIScroll;
        vertHori.ScrollHorizontal = MainViewScript.Instance.UIScroll;
        
    }



    private static ClanMgr _inst = null;

    public static ClanMgr Instance
    {
        get
        {
            if(null == _inst)
            {
                _inst = FindObjectOfType<ClanMgr>();
                if(null == _inst)
                {
                    Debug.LogError("클랜 매니저가 없습니다.");
                }
            }

            return _inst;
        }
    }
}
