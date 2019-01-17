using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SHAKE_DIR
{
    SHAKE_LEFT,
    SHAKE_RIGHT,
    SHAKE_END
}

public class ShakeObjectScript : MonoBehaviour 
{

    public int Angle = 1;
    public int Speed = 15;

    Transform       _trans = null;
    CardScript      _card = null;
    UISprite        _sprUI = null;
    UIButtonScale   _btnScale = null;

    SHAKE_DIR _dir = SHAKE_DIR.SHAKE_END;

    public void SetShakeDir(SHAKE_DIR dir)
    {
        _dir = dir;
    }

    private void checkMouseHover()
    {
        if (null == _sprUI)
            return;

        Vector2 mousePos = Input.mousePosition;
        Vector2 rectStart = new Vector2(_trans.localPosition.x - _sprUI.width * 0.5f, _trans.localPosition.y - _sprUI.height * 0.5f);
        rectStart.x += Screen.width * 0.5f;
        rectStart.y += Screen.height * 0.5f;
        Rect rect = new Rect(rectStart,new Vector2(_sprUI.width, _sprUI.height) );

        Vector3 scale = _trans.localScale;

        if (true == rect.Contains(mousePos))
        {
            CardScript cardFrom = DackMgr.Instance.ChangeCard_from;
            if(null != cardFrom && _card != cardFrom)
            {
                DackMgr.Instance.SetChangeCard_to(_card);
            }

            scale.x += Time.deltaTime * 1f;
            scale.y += Time.deltaTime * 1.0f;
            if (scale.x >= 1.1f)
            {
                scale.x = 1.1f;
                scale.y = 1.1f;
            }
        }

        else
        {
            if( _card == DackMgr.Instance.ChangeCard_to )
            {
                DackMgr.Instance.SetChangeCard_to(null);
            }

            scale.x -= Time.deltaTime * 1.0f;
            scale.y -= Time.deltaTime * 1.0f;
            if (scale.x <= 1.0f)
            {
                scale.x = 1.0f;
                scale.y = 1.0f;
            }
        }
        _trans.localScale = scale;
    }

    private void OnEnable()
    {
        _trans = GetComponent<Transform>();
        _card = GetComponent<CardScript>();
        _sprUI = GetComponent<UISprite>();
        _btnScale = GetComponent<UIButtonScale>();

        // 버튼의 OnHover()가 OnDrag()일때는 호출이 안되서 이때는 끄고 직접 만들어줬다.
        _btnScale.enabled = false;
       
        //지그재그로 흔들리는 방향을 반대로 해주기 위해 설정
        switch(_card.Idx)
        {
            case 0:
            case 2:
            case 5:
            case 7:
                _dir = SHAKE_DIR.SHAKE_LEFT;
                break;

            case 1:
            case 3:
            case 4:
            case 6:
                _dir = SHAKE_DIR.SHAKE_RIGHT;
                break;
        }
    }

    // Update is called once per frame
    void Update () 
    {
        Vector3 rotation = _trans.rotation.eulerAngles;

        if (SHAKE_DIR.SHAKE_LEFT == _dir)
        {
            rotation.z -= Time.deltaTime * Speed;
            if(rotation.z > Angle && rotation.z < 360 - Angle)
            {
                rotation.z = 360 - Angle;
                _dir = SHAKE_DIR.SHAKE_RIGHT;
            }
        }

        else if(SHAKE_DIR.SHAKE_RIGHT == _dir)
        {
            rotation.z += Time.deltaTime * Speed;
            if (rotation.z < 360 - Angle && rotation.z > Angle)
            {
                rotation.z = Angle;
                _dir = SHAKE_DIR.SHAKE_LEFT;
            }
        }
        _trans.eulerAngles = rotation;

        checkMouseHover();
	}
}
