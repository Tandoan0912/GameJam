using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 startTouch, swipeDelta;
    private bool isDragin;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    public bool isMove = true;
    public bool isOver = false;

    void Update()
    {
        tap = swipeDown = swipeLeft = swipeRight = swipeUp = false;

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;          
            isDragin = true;
            startTouch = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragin = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragin = false;
                Reset();
            }
        }

        swipeDelta = Vector2.zero;
        if (isDragin)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
            //isDragin = false;
        }
        if (swipeDelta.magnitude > 125 && isMove == true && isOver == false)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                    Debug.Log("swipeLeft");
                }
                else
                {
                    swipeRight = true;
                    Debug.Log("swipeRight");
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    Debug.Log("swipeDown");
                }
                else
                {
                    swipeUp = true;
                    Debug.Log("swipeUp");
                }
            }

            Reset();
        }
    }


    public void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragin = false;
    }
}
