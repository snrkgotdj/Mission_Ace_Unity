using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DackScript : MonoBehaviour {

    public int DackStartX;
    public int DackStartY;
    public int DackPadX;
    public int DackPadY;

    List<CardScript> _listDackCard = new List<CardScript>();

    UISprite _sprCardPrefab = null;

    bool IsSelected { get; set; } 


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

        _listDackCard[dackCard.Idx] = holdCard;
        holdCard.transform.parent = gameObject.transform;

        return true;
    }

    public bool ChangeCardInDack(CardScript cardFrom, CardScript cardTo)
    {
        if (null == cardFrom)
            return false;

        if (null == cardTo)
            return false;

        if (false == cardFrom.IsDack || false == cardFrom.IsDack)
            return false;

        CardScript tmp = _listDackCard[cardFrom.Idx];
        _listDackCard[cardFrom.Idx] = _listDackCard[cardTo.Idx]; 
        _listDackCard[cardTo.Idx] = tmp;

        RenewCardPosition();
        return true;
    }

    public void CancleSelect()
    {
        foreach (var card in _listDackCard)
        {
            card.ReturnNaturally();
        }

        GetComponent<UICenterOnChild>().enabled = false;
    }

    public void ReadyToChange()
    {
        foreach(var card in _listDackCard)
        {
            card.GetComponent<ShakeObjectScript>().enabled = true;
        }
    }

    public void RenewCardPosition() // 위치 재셋팅 함수
    {
        Vector3 pos = new Vector3(DackStartX, DackStartY);

        for (int i = 0; i < _listDackCard.Count; ++i)
        {
            int idxX = i % 4;
            int idxY = i / 4;

            pos.x = DackStartX + (DackPadX + _sprCardPrefab.width) * idxX;
            pos.y = DackStartY - (DackPadY + _sprCardPrefab.height) * idxY;

            //_listDackCard[i].transform.localPosition = pos;
            _listDackCard[i].MoveTo(pos);
            _listDackCard[i].Idx = i;

            _listDackCard[i].IsDack = true;
        }
    }

    private void createDackCard()
    {
        for (int i = 0; i < 8; ++i)
        {
            GameObject obj = gameObject.AddChild(_sprCardPrefab.gameObject);
            CardScript card = obj.GetComponent<CardScript>();
            _listDackCard.Add(card);

            card.IsDack = true;
        }
        RenewCardPosition();
    }

    void Start()
    {
        _sprCardPrefab = DackMgr.Instance.CardPrefab.GetComponent<UISprite>();

        createDackCard();

        IsSelected = false;
    }
}
