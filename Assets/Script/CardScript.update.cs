using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardScript : MonoBehaviour {
    public enum CARD_STATE
    {
        CARD_DISAPPEAR,
        CARD_APPEAR,
        CARD_MOVECENTER,

        CARD_END
    }

    private void Update()
    {
        switch (_cardState)
        {
            case CARD_STATE.CARD_DISAPPEAR:     disappearCard();        break;
            case CARD_STATE.CARD_APPEAR:        appearCard();           break;
            case CARD_STATE.CARD_MOVECENTER:    cardMoveCenter();       break;
        }
    }


    public void SetState(CARD_STATE state)
    {
        _cardState = state;
        switch(_cardState)
        {
            case CARD_STATE.CARD_APPEAR:        _sprCard.alpha = 0;     break;
            case CARD_STATE.CARD_DISAPPEAR:     _sprCard.alpha = 1;     break;
        }
    }


    private void disappearCard()
    {
        _sprCard.alpha -= Time.deltaTime;
        if (_sprCard.alpha <= 0)
        {
            _sprCard.alpha = 0;
            _cardState = CARD_STATE.CARD_END;
        }
    }

    private void appearCard()
    {
        _sprCard.alpha += Time.deltaTime;
        if (_sprCard.alpha >= 1)
        {
            _sprCard.alpha = 1;
            _cardState = CARD_STATE.CARD_END;
        }
    }

    private void cardMoveCenter()
    {
        Vector3 centerPos = DackMgr.Instance.CenterPos.position;
        _trans.position = Vector3.Lerp(_trans.position, centerPos, Time.deltaTime * 6);

        Vector3 dist = _trans.position - centerPos;
        if(dist.magnitude < 0.01)
        {
            _trans.position = centerPos;
            _cardState = CARD_STATE.CARD_END;
        }
    }
}
