// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhonePong.VSRetro.Battle
{
    public class VSRetroBattleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected bool interactable;
        [SerializeField] protected float holdDuration;
        protected const float tooltipHoldDuration = 1f;

        protected Coroutine currentHoldTimeCoroutine;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable) return;

            currentHoldTimeCoroutine = StartCoroutine(HoldTimeCoroutine());
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable) return;

            if (currentHoldTimeCoroutine != null)
            {
                StopCoroutine(currentHoldTimeCoroutine);
                currentHoldTimeCoroutine = null;

                if (holdDuration < tooltipHoldDuration)
                {
                    SelectThis();
                }
                else
                {
                    ShowTooltip();
                }
            }

            holdDuration = 0f;
        }

        public virtual void SetInteractable(bool value) => interactable = value;

        protected virtual IEnumerator HoldTimeCoroutine()
        {
            while (interactable)
            {
                holdDuration += Time.deltaTime;
                yield return null;
            }
        }

        protected virtual void SelectThis()
        {
            
        }

        protected virtual void ShowTooltip()
        {
            
        }
    }
}