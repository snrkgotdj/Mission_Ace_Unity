using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGroup : MonoBehaviour 
{
    // public
    public GameObject PlusEvent;
    public GameObject EventPrefab = null;
    public int EventStartPosY;

    public int Height { get; set; }


    //private
    List<GameObject> _listEvent = new List<GameObject>();

    UISprite _sprTitle;
    UISprite _sprEventPrefab;
    

    int _eventCount = 0;



    private void OnEnable()
    {
        _sprTitle = GetComponent<UISprite>();
        _sprEventPrefab = EventPrefab.GetComponent<UISprite>();

        Height = (int)(_sprTitle.localSize.y + (_sprEventPrefab.localSize.y * _eventCount));

        CreateEvent();
        CreateEvent();

        EventGroupMgr.Instance.AddEventGroup(this);
    }

    public void SetTouch(bool bTouch)
    {
        for(int i = 0; i < _listEvent.Count; ++i)
        {
            _listEvent[i].GetComponent<BoxCollider>().enabled = bTouch;
        }
        PlusEvent.GetComponent<BoxCollider>().enabled = bTouch;
    }

    public void CreateEvent()
    {
        if (null == EventPrefab)
            Debug.LogError("EventPrefab이 없습니다. 등록해주세요");

        GameObject instance = gameObject.AddChild(EventPrefab);

        int iPosY = EventStartPosY - (int)_sprEventPrefab.localSize.y * _eventCount;

        Height += (int)_sprEventPrefab.localSize.y;
        _eventCount += 1;

        instance.GetComponent<VertHoriScrollScript>().ScrollVertical = EventGroupMgr.Instance.GetComponent<UIScrollView>();
        instance.GetComponent<VertHoriScrollScript>().ScrollHorizontal = MainViewScript.Instance.GetComponent<UIScrollView>();

        instance.transform.localPosition = new Vector3(transform.position.x, iPosY);
        instance.GetComponent<EventScript>().SetTitle("Event_" + _eventCount.ToString());

        _listEvent.Add(instance);
        EventGroupMgr.Instance.RenewEventGroup();
    }
}
