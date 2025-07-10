// System
using System;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("가상 조이스틱")]
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected RectTransform lever;

    /// <summary>
    /// 레버가 이동 가능한 범위
    /// </summary>
    [SerializeField] protected float leverRange;

    /// <summary>
    /// 레버가 이동 가능한 범위를 정해주는 변수(수가 커질수록 더 넓은 범위로 이동 가능)
    /// </summary>
    [SerializeField][Range(0f, 1f)] protected float leverRangeOffset = 0.5f;

    protected Vector2 leverDirection
    {
        get
        {
            if (rectTransform && lever) return ((Vector2)(lever.position - rectTransform.position)).normalized;
            else
            {
                Debug.LogError($"NullException: rectTransform: {rectTransform ?? null}, lever: {lever ?? null}");

                return Vector2.zero;
            }
        }
    }

    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        leverRange = rectTransform.sizeDelta.x * leverRangeOffset;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        DragLever(GetLeverPos(eventData.position, rectTransform.position));
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        DragLever(GetLeverPos(eventData.position, rectTransform.position));
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

    protected virtual void DragLever(Vector2 newLeverPos)
    {
        lever.anchoredPosition = newLeverPos;
    } 

    protected virtual Vector2 GetLeverPos(Vector2 eventPosition, Vector2 joystickPos)
    {
        Vector2 newLeverPos = eventPosition - joystickPos;
        newLeverPos = newLeverPos.sqrMagnitude <= leverRange * leverRange
        ? newLeverPos : newLeverPos.normalized * leverRange;

        return newLeverPos;
    }
}
