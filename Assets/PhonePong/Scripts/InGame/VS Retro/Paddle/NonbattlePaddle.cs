// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// PhonePong
using PhonePong.Layer;
using Unity.VisualScripting;

namespace PhonePong.VSRetro.Nonbattle
{
    public class NonbattlePaddle : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private float moveSpeed;

        [Header("선택(상호작용)")]
        [SerializeField] private List<Transform> interactionableTransformList = new List<Transform>();
        private const int interactableLayer = LayerDatas.interactableLayer;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 movement)
        {
            rb2D.MovePosition(rb2D.position + (movement * moveSpeed));
        }

        public void OnSelect()
        {
            IInteractable interactable = GetNearestIntertable();

            if (interactable == null) return;

            interactable.Interact();
        }

        private IInteractable GetNearestIntertable()
        {
            float minDistance = float.MaxValue;
            IInteractable nearestInteractable = null;

            foreach (Transform interactionableTransform in interactionableTransformList)
            {
                float distance = Vector2.Distance(transform.position, interactionableTransform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestInteractable = interactionableTransform.GetComponent<IInteractable>();
                }
            }
            
            return nearestInteractable;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == interactableLayer)
            {
                interactionableTransformList.Add(collision.transform);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == interactableLayer)
            {
                interactionableTransformList.Remove(collision.transform);
            }
        }
    }
}