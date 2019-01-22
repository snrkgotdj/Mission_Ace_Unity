using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertHoriScrollScript : UIDragScrollView
{
    public UIScrollView ScrollHorizontal;
    public UIScrollView ScrollVertical;

    private Vector3 _firstMouse;
    private bool _isMove = false;

    public bool IsMove 
    { 
        get { return _isMove; } 
    }

    protected override void OnPress(bool pressed)
    {
        // 마우스가 올라왔을땐 그대로 호출
        if (false == pressed)
        {
            base.OnPress(false);
            _isMove = false;
            return;
        }

        _firstMouse = Input.mousePosition;
    }

    protected override void OnDrag(Vector2 delta)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseDiff = mousePos - _firstMouse;

        // 드래그 대상선정이 완료됬으면 드래그 호출
        if (true == _isMove) 
        {
            base.OnDrag(delta);
            return;
        }

        // 드래그 대상을 선정한다
        if (mouseDiff.x * mouseDiff.x < mouseDiff.y * mouseDiff.y)
        {
            scrollView = ScrollVertical;
        }
        else
        {
            scrollView = ScrollHorizontal;
        }

        base.OnPress(true);
        base.OnDrag(delta);
        _isMove = true;
    }

}
