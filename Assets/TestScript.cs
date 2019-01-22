using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void OnClick()
    {
        print("OnClick()");
    }

    void OnHover(bool isOver)
    {
        if (true == isOver)
            print("OnHOver(true");

        if (false == isOver)
            print("OnHOver(false)");
    }

    void OnSelect(bool selected)
    {
        if (true == selected)
            print("OnSelect(true");

        if (false == selected)
            print("OnSelect(false)");
    }

    void OnPress(bool isPress)
    {
        if(true == isPress)
            print("Press(true");

        if (false == isPress)
            print("Press(false)");


    }

    void OnDragStart()
    {
        print("DragStart");
    }

    void OnDrag(Vector2 delta)
    {
        print("OnDrag");
    }


    // Update is called once per frame
    void Update () 
    {
        if (true == Input.GetMouseButtonUp(0))
            print("MouseButtonUp");

        if (true == Input.GetMouseButtonDown(0))
            print("MouseButtonDown");


    }
}
