using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCardScript : MonoBehaviour {

    public int DackStartX;
    public int DackStartY;
    public int DackPadX;
    public int DackPadY;

    public int FirstCardCount;

    List<CardScript> _listCard = new List<CardScript>();
    UISprite _sprCardPrefab = null;

    public bool ExchangeCard(CardScript fromCard, CardScript toCard)
    {
        if (null == fromCard)
            return false;

        if (null == toCard)
            return false;

        CardScript dackCard = null;
        CardScript holdCard = null;

        if (true == fromCard.IsDack)
        {
            dackCard = fromCard;
            holdCard = toCard;
        }
        else if (true == toCard.IsDack)
        {
            dackCard = toCard;
            holdCard = fromCard;
        }

        _listCard[holdCard.Idx] = dackCard;
        dackCard.transform.parent = gameObject.transform;

        return true;
    }

    public void SelectThisCard(CardScript card)
    {
        foreach(CardScript holdCard in _listCard)
        {
            if (holdCard == card)
                continue;

            holdCard.SetState(CardScript.CARD_STATE.CARD_DISAPPEAR);
        }
    }

    public void CancleSelect()
    {
        foreach (CardScript holdCard in _listCard)
        {
            holdCard.SetState(CardScript.CARD_STATE.CARD_APPEAR);
            holdCard.ReturnNaturally();
        }
    }

    public void SetTouch(bool bTouch)
    {
        foreach(CardScript card in _listCard)
        {
            card.GetComponent<BoxCollider>().enabled = bTouch;
        }
    }


    public void RenewPosition()
    {
        //코스트를 기준으로 정렬
        _listCard.Sort(delegate (CardScript x, CardScript y)
        {
            if (x.Cost > y.Cost) return 1;
            else if(x.Cost < y.Cost) return -1;
            return 1;
        });

        //위치 셋팅 및 인덱스 셋팅
        Vector3 pos = new Vector3(DackStartX, DackStartY);
        for (int i = 0; i < _listCard.Count; ++i)
        {
            int idxX = i % 4;
            int idxY = i / 4;

            pos.x = DackStartX + (DackPadX + _sprCardPrefab.width) * idxX;
            pos.y = DackStartY - (DackPadY + _sprCardPrefab.height) * idxY;

            _listCard[i].transform.localPosition = pos;
            _listCard[i].Idx = i;
            _listCard[i].IsSelected = false;
            _listCard[i].IsDack = false;
        }
    }

    private void initCard()
    {
        for (int i = 0; i < FirstCardCount; ++i)
        {
            GameObject obj = gameObject.AddChild(_sprCardPrefab.gameObject);
            _listCard.Add(obj.GetComponent<CardScript>());
        }

        RenewPosition();
    }

    void Start()
    {
        _sprCardPrefab = DackMgr.Instance.CardPrefab.GetComponent<UISprite>();

        initCard();
    }

}
