using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUseScript : MonoBehaviour {

    public CardScript Card { get; set; }

    Transform _trans = null;

    public void UseCard()
    {
        DackMgr.Instance.SelectThisCard(Card);
        DackMgr.Instance.BattleDack.GetComponent<UICenterOnChild>().enabled = true;

        Card.SetState(CardScript.CARD_STATE.CARD_MOVECENTER);
        Card.IsSelected = true;
        Card.GetComponent<BoxCollider>().enabled = true;

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _trans = GetComponent<Transform>();
        _trans.localPosition = new Vector3(0, 100, 0);
    }

    private void Update()
    {
        // 다른곳을 클릭했을때 ( Update 가 Onclick보다 늦게 호출된다)
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
            MainViewScript.Instance.SetNextTouchOn();
        }
    }
}
