using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public int LayerInfo(string layerName);
    public bool Tab(int layerMask);
    public bool Tab();
    public void Drag();
}

public class PlayerInput : MonoBehaviour, IInput
{
    private Ray ray;
    private RaycastHit hit;
    private int layerMask;

    public float distance = 5f;

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        layerMask = LayerInfo("Object");

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (Tab(layerMask))
                {
                    hit.transform.GetComponent<Tiger>()?.Die();
                }
                break;
            case TouchPhase.Moved:
                if (Tab(layerMask))
                {
                    // 오브젝트 움직이기 위한 코루틴 호출
                    // 근데 굳이 코루틴이 필요할까...?
                }
                break;
            case TouchPhase.Ended:
                if (Tab(layerMask))
                {
                    // 이전  phase가 move였다면 코루틴 중단
                }
                break;
        }
        /*
        // 터치
        if (touch.phase == TouchPhase.Began)
        {
            layerMask = LayerInfo("Object");
            if (Tab(layerMask))
            {
                hit.transform.GetComponent<Tiger>()?.Die();
            }
        }
        //드래그
        if (touch.phase == TouchPhase.Moved)
        {
            layerMask = LayerInfo("Default");
            if (Tab(layerMask))
        }

        if (touch.)
        */
    }

    /// <summary>
    /// 몇 번째 레이어인지 반환
    /// </summary>
    /// <param name="layerName">레이어 이름</param>
    /// <returns></returns>
    public int LayerInfo(string layerName)
    {
        // 나중에는 메서드가 아니라 class로 사용해야할 애니메이션 미리 연산해두고 쓸 것
        // ex ) 수업때 배웠던 AnimID class 같은
        layerMask = 1 << LayerMask.NameToLayer(layerName);

        return layerMask;
    }

    // 터치해서 만약 오브젝트가 있다면 true, 없다면 false 반환하는 형태로 가야할지
    public bool Tab()
    {
        return Tab(1);
    }

    public bool Tab(int layerMask)
    {
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Drag()
    {
        
    }
}