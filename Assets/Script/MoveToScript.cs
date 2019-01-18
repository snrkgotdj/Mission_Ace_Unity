using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToScript : MonoBehaviour {

    public float Speed;

    Vector3 _dir = Vector3.zero;
    Vector3 _desPosition = Vector3.zero;

    Transform _trans = null;

    public void SetMoveTo(Vector3 desPosition)
    {


        _desPosition = desPosition;

        _dir = _desPosition - _trans.localPosition;
        _dir.Normalize();

        enabled = true;
    }

    private void Awake()
    {
        enabled = false;
        _trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () 
    {
        _trans.localPosition += _dir * Speed * Time.deltaTime;

        Vector3 dir = _desPosition - _trans.localPosition;
        dir.Normalize();

        // 목적지 도달하거다 더이상 갔다.
        if(dir == -_dir || dir == Vector3.zero)
        {
            _trans.localPosition = _desPosition;
            enabled = false;
            return;
        }
	}
}
