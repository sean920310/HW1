using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollSliderWithMiddleMouseButton : MonoBehaviour, IScrollHandler
{
    public void OnScroll(PointerEventData eventData)
    {
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
