using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform _joystick;
    [SerializeField] RectTransform _handle;
    private Vector2 _inputVector;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystick, 
            eventData.position, 
            eventData.pressEventCamera, 
            out position
        );

        position = Vector2.ClampMagnitude(position, _joystick.sizeDelta.x / 2);

        _handle.anchoredPosition = position;
        _inputVector = position / (_joystick.sizeDelta.x / 2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handle.anchoredPosition = Vector2.zero;
        _inputVector = Vector2.zero;
    }

    public float Horizontal()
    {
        return _inputVector.x;
    }

    public float Vertical()
    {
        return _inputVector.y;
    }
}
