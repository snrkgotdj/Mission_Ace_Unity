using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGroupMgr : MonoBehaviour 
{
    public int EventGroupStartY;
    public Transform PageEvent { get; private set; }

    List<EventGroup> _listEventGroup = new List<EventGroup>();
    Transform _trans = null;


    private void Start()
    {
        _trans = GetComponent<Transform>();
        PageEvent = _trans.parent;

        RenewEventGroup();
    }

    public void SetTouch(bool bTouch)
    {
        for(int i = 0; i < _listEventGroup.Count; ++i)
        {
            _listEventGroup[i].SetTouch(bTouch);
        }


    }

    public void AddEventGroup(EventGroup _evnetGroup)
    {
        _listEventGroup.Add(_evnetGroup);
    }


    public void RenewEventGroup()
    {
        int posY = EventGroupStartY;

        for(int i = 0; i < _listEventGroup.Count; ++i)
        {
            _listEventGroup[i].transform.localPosition = new Vector3(0, posY);
            posY -= _listEventGroup[i].Height;
        }
    }



    /// <summary>
    /// SingleTon 코드
    /// </summary>
    static private EventGroupMgr _inst = null;
    static public EventGroupMgr Instance
    {
        get
        {
            if(null == _inst)
            {
                _inst = FindObjectOfType<EventGroupMgr>() as EventGroupMgr;
                if(null == _inst)
                {
                    Debug.LogError("EventGroupMgr이 없습니다.");
                }
            }
            return _inst;
        }
    }
}
