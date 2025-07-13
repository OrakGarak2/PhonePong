// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;

// PhonePong
using PhonePong.VSRetro.Tooltip;

namespace PhonePong.VSRetro.Battle
{
    public class VSRetroBattleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected bool interactable;

        [Header("툴팁")]
        protected Coroutine currentHoldTimeCoroutine;
        protected TooltipUser tooltipUser;

        protected const float tooltipHoldDuration = 1f;
        [SerializeField] protected bool onTooltip;
        [SerializeField] protected Vector2 tooltipPosOffset = new Vector2(-475f, 0f);
        [SerializeField] protected string tooltipContentName;
        [SerializeField] protected string tooltipContentDesc;

        protected virtual void Start()
        {
            SetInteractable(true);
        }

        public virtual void SetInteractable(bool value) => interactable = value;

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
            }

            if (!onTooltip)
            {
                SelectThis();
            }
            else
            {
                HideTooltip();
            }
        }

        protected virtual IEnumerator HoldTimeCoroutine()
        {
            float holdDuration = tooltipHoldDuration;

            while (interactable)
            {
                holdDuration -= Time.deltaTime;

                if (holdDuration < 0)
                {
                    ShowTooltip();
                    break;
                }

                yield return null;
            }
        }

        protected virtual void SelectThis()
        {

        }

        protected virtual void ShowTooltip()
        {
            if (tooltipUser == null)
            {
                SetTooltipUser();
            }

            onTooltip = true;
            tooltipUser.ShowTooltip();
        }

        protected virtual void HideTooltip()
        {
            onTooltip = false;
            tooltipUser.HideTooltip();
        }

        protected virtual void SetTooltipUser()
        {
            tooltipUser = new TooltipUser((Vector2)transform.position + tooltipPosOffset, tooltipContentName, tooltipContentDesc);
        }
    }
}