using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DackMgr : MonoBehaviour {

    public GameObject CardPrefab = null;
    public Transform CenterPos = null;

    public DackScript BattleDack { get; private set; }
    public HoldCardScript HoldCard { get; private set; }

    public CardScript SelectedCard { get; private set; }
    public CardScript ChangeCard_from { get; private set; }
    public CardScript ChangeCard_to { get; private set; }

    public void SetSelectedCard(CardScript card) { SelectedCard = card; }
    public void SetChangeCard_from(CardScript card) { ChangeCard_from = card; }
    public void SetChangeCard_to(CardScript card) { ChangeCard_to = card; }



    public void CancleSelect()
    {
        if(null != ChangeCard_from)
            ChangeCard_from.IsSelected = false;

        if(null != ChangeCard_to)
            ChangeCard_to.IsSelected = false;

        SelectedCard = null;
        ChangeCard_from = null;
        ChangeCard_to = null;

        MainViewScript.Instance.SetNextTouchOn();

        BattleDack.RenewCardPosition();
        BattleDack.CancleSelect();

        HoldCard.RenewPosition();
        HoldCard.CancleSelect();
    }


    public bool ChangeCard()
    {
        // 바꿀 카드가 셋팅이 안되있으면 리턴
        if (null == ChangeCard_from) 
            return false;

        if (null == ChangeCard_to)
            return false;

        // 둘중 하나는 덱에 있는 카드여야한다. 아니면 리턴
        if (false == ChangeCard_from.IsDack && false == ChangeCard_to.IsDack) 
            return false;

        //둘다 덱에 있는 카드끼리 교환
        if(true == ChangeCard_from.IsDack && true == ChangeCard_to.IsDack)
        {
            if(false == BattleDack.ChangeCardInDack(ChangeCard_from, ChangeCard_to))
            {
                Debug.LogError("덱 안의 카드끼리 교환 실패");
                Debug.Assert(false);
                return false;
            }
            return true;
        }

        // 덱과 홀드중 카드의 교환
        // 덱에서 먼저 카드를 빼준 후 카드 홀더에 넣어준다.
        BattleDack.ExchangeCard(ChangeCard_from, ChangeCard_to);
        HoldCard.ExchangeCard(ChangeCard_from, ChangeCard_to);

        BattleDack.RenewCardPosition();
        HoldCard.RenewPosition();

        CancleSelect();

        return true;
    }

    public void SelectThisCard(CardScript card)
    {
        SelectedCard = card;

        BattleDack.ReadyToChange();
        HoldCard.SelectThisCard(card);
    }

    public void SetTouch(bool bTouch)
    {
        HoldCard.SetTouch(bTouch);
    }

    void Start () 
    {
        CardPrefab.GetComponent<VertHoriScrollScript>().ScrollVertical = GetComponent<UIScrollView>();
        CardPrefab.GetComponent<VertHoriScrollScript>().ScrollHorizontal = MainViewScript.Instance.GetComponent<UIScrollView>();

        BattleDack = GetComponentInChildren<DackScript>();
        HoldCard = GetComponentInChildren<HoldCardScript>();

        SelectedCard = null;
        ChangeCard_from = null;
        ChangeCard_to = null;

    }


    static private DackMgr inst = null;
    static public DackMgr Instance
    {
        get
        {
            if(null == inst)
            {
                inst = FindObjectOfType<DackMgr>();
                if(null == inst)
                {
                    Debug.LogError("There is No DackMgr");
                }
            }
            return inst;
        }
    }
}
