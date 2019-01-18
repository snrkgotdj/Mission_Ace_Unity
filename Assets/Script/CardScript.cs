using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardScript : MonoBehaviour {

    [SerializeField]
    private GameObject CardUse = null;

    public int  Cost { get; set; }
    public int  Idx { get; set; }
    public bool IsDack { get; set; }
    public bool IsSelected { get; set; }

    Transform       _trans = null;
    UISprite        _sprCard = null;
    UISprite        _sprCardUse = null;
    UIButtonScale   _btnScale = null;
    UICenterOnChild _centerOnChild = null;
    UIDragDropItem  _dragDrop = null;

    ShakeObjectScript   _shakeScript = null;
    MoveToScript        _moveTo = null;


    CARD_STATE _cardState = CARD_STATE.CARD_END;

    public void SetDragDrop(bool bOn)
    {
        if(null == _dragDrop)
        {
            Debug.LogError("Card에 DragDrop이 없습니다.");
            Debug.Assert(false);
            return;
        }

        _dragDrop.enabled = bOn;
    }

    public void MoveTo(Vector3 position)
    {
        if(null == _moveTo)
        {
            Debug.LogError("Card에 MoveToScript가 없습니다. 셋팅후 사용해주세요");
            Debug.Assert(false);
            return;
        }

        _moveTo.SetMoveTo(position);
    }


    public void ReturnNaturally()
    {
        _trans.eulerAngles = Vector3.zero;


        _btnScale.enabled = true;
        _centerOnChild.enabled = false;
        _shakeScript.enabled = false;
    }

    private void clickDackToChange()
    {
        CardScript selectCard = DackMgr.Instance.SelectedCard;
        if (null == selectCard)
            return;

        DackMgr.Instance.SetChangeCard_from(selectCard);
        DackMgr.Instance.SetChangeCard_to(this);

        DackMgr.Instance.ChangeCard();
    }

    private void clickToChange()
    {
        DackMgr.Instance.SetChangeCard_from(this);


    }

    private void moveCardCanSee()
    {
        // bottom = 덱의 위치 + 카드의 위치 + 카드 사용창의 위치  - 카드 사용창 크기
        int posY = (int)(DackMgr.Instance.transform.localPosition.y + _trans.localPosition.y + CardUse.transform.localPosition.y);
        posY -= _sprCardUse.height;

        int bottom = (int)(PageBtnMgr.Instance.SelectSizeY - Screen.height * 0.5);

        if (posY < bottom)
        {
            gameObject.GetComponent<UICenterOnChild>().enabled = true;
        }
    }

    private void clickToSelect()
    {
        //카드 사용창을 띄운다.
        GameObject obj = gameObject.AddChild(CardUse);
        obj.transform.localPosition = new Vector3(0, 100, 0);
        obj.GetComponent<CardUseScript>().Card = this;

        //카드 크기를 원상복구한다.
        _btnScale.OnDisable();

        //나머지 카드들의 클릭을 끈다.
        MainViewScript.Instance.SetTouch(false);

        //카드가 아래있으면 카드를 볼수 있는 위치로 올려준다.
        moveCardCanSee();

        //덱에 저장해두기
        DackMgr.Instance.SetSelectedCard(this);
    }

    void OnPress(bool isPress)
    {
        if (true == isPress)
            return;

        // 카드를 바꾼다.
        if (null != DackMgr.Instance.ChangeCard_to)
        {
            DackMgr.Instance.ChangeCard();
            return;
        }

        // 집었던 카드가 덱에 있던 카드면 위치만 돌려놓는다.
        if (true == IsDack)
        {
            DackMgr.Instance.BattleDack.RenewCardPosition();
        }


        // 카드를 바꾸지 않고 원상태로 돌린다.
        if (true == IsSelected)
        {
            DackMgr.Instance.CancleSelect();
        }
    }

    void OnClick()
    {
        if (true == IsDack)
        {
            clickDackToChange();
            return;
        }

        if (true == IsSelected)
        {
            clickToChange();
            return;
        }

        else
        {
            clickToSelect();
        }
    }

    void OnDrag(Vector2 delta)
    {
        if(null == DackMgr.Instance.SelectedCard)
        {
            _btnScale.OnDisable();
            return;
        }

        //드래그하면 원래 상태로 돌린.
        _cardState = CARD_STATE.CARD_END;

        DackMgr.Instance.SetChangeCard_from(this);

    }

    void OnEnable()
    {
        if (null == CardUse)
        {
            Debug.LogError("CardUse 프리팹이 없습니다. 넣어주세요");
            Debug.Assert(false);
        }

        IsSelected = false;

        _sprCard = GetComponent<UISprite>();
        _sprCardUse = CardUse.GetComponent<UISprite>();
        _btnScale = GetComponent<UIButtonScale>();
        _trans = GetComponent<Transform>();
        _centerOnChild = GetComponent<UICenterOnChild>();
        _dragDrop = GetComponent<UIDragDropItem>();

        _shakeScript = GetComponent<ShakeObjectScript>();
        _moveTo = GetComponent<MoveToScript>();

        Cost = Random.Range(1, 9);
        _sprCard.spriteName = "Card_" + Cost.ToString();
    }
}
