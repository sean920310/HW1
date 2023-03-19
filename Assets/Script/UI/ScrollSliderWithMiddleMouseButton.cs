using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int X, int Y)
    {
        SetCursorPos(X, Y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        mouse_event
            ((int)value,
            position.X,
             position.Y,
             0,
             0)
             ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

}
public class ScrollSliderWithMiddleMouseButton : MonoBehaviour, IScrollHandler
{
    //TestInputModule tim;
    private void Update()
    {
        bool isMouseDown = false;
        Vector3 MousePos = Vector3.zero;
        if (Input.GetMouseButtonDown(((int)MouseButton.LeftMouse)))
        {
            isMouseDown = true;
            MousePos = Input.mousePosition;
        }
        
        if(isMouseDown && (MousePos - Input.mousePosition).magnitude >= 1 * Time.deltaTime)
        {
            gameObject.SetActive(false);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
            //tim.ClickAt(MousePos.x, MousePos.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        gameObject.SetActive(true);
        float scrollDelta = eventData.scrollDelta.y;
        if (scrollDelta != 0)
        {
            Scrollbar slider = GetComponent<Scrollbar>();
            if (slider != null)
            {
                float currentValue = slider.value;
                float newValue = currentValue + Mathf.Sign(scrollDelta) * 0.05f;
                slider.value = Mathf.Clamp(newValue, 0f, 1f);
            }
        }
    }
}