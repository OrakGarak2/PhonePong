// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    [Header("라인 라켓")]
    [SerializeField] private GameObject lineRacketPrefab;
    [SerializeField] private GameObject currentLineRacket;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private List<Vector2> pointPosList;

    [SerializeField] private int layerBitMask;

    private const float nextLinePointMinDistance = 0.15f;
    private const int maxTouchCount = 4;

    [Header("게이지")]
    [SerializeField] private Image lineGauge;
    private const float lineGaugeMaxAmount = 1f;
    [SerializeField] private float lineGaugeAmount = lineGaugeMaxAmount;
    [SerializeField] private float lineGaugeIncreaseRate;
    private const float lineMaxDistance = 4f;

    private void Start()
    {
        layerBitMask = 1 << gameObject.layer;
    }

    private void Update()
    {
        if (CheckPointInZone(out Vector2 pointPos))
        {
            if (currentLineRacket == null)
            {
                CreateLine(pointPos);
            }
            else
            {
                float distance = Vector2.Distance(pointPos, pointPosList[pointPosList.Count - 1]);
                if (lineGaugeAmount > 0f && distance > nextLinePointMinDistance)
                {
                    UpdateLine(pointPos, distance);
                }
            }
        }
        else
        {
            DeleteLine();

            if (lineGaugeAmount != lineGaugeMaxAmount)
            {
                UpdateLineGauge(lineGaugeAmount + lineGaugeIncreaseRate);
            }
        }
    }

    private bool CheckPointInZone(out Vector2 pointPos)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (i >= maxTouchCount) break;

            pointPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            Collider2D hit = Physics2D.OverlapPoint(pointPos, layerBitMask);

            Debug.Log($"Object: {gameObject.name}\ni: {i}, pointPos: {pointPos}, hit: {hit!=null}, hit && hit.gameObject == gameObject: {hit && hit.gameObject == gameObject}");

            if (hit && hit.gameObject == gameObject)
            {
                return true;
            }
        }

        pointPos = Vector2.zero;
        return false;
    }

    private void CreateLine(Vector2 pointPos)
    {
        currentLineRacket = Instantiate(lineRacketPrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLineRacket.GetComponent<LineRenderer>();
        edgeCollider2D = currentLineRacket.GetComponent<EdgeCollider2D>();

        pointPosList.Clear();
        pointPosList.Add(pointPos);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, pointPosList[0]);

        edgeCollider2D.points = pointPosList.ToArray();

        edgeCollider2D.enabled = true;
    }

    private void UpdateLine(Vector2 newPointPos, float distance)
    {
        UpdateLineGauge(lineGaugeAmount - distance / lineMaxDistance);

        pointPosList.Add(newPointPos);

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPointPos);

        edgeCollider2D.points = pointPosList.ToArray();
    }

    private void DeleteLine()
    {
        if (currentLineRacket != null)
        {
            Destroy(currentLineRacket);
        }
    }

    private void UpdateLineGauge(float newAmount)
    {
        lineGaugeAmount = Mathf.Clamp(newAmount, 0f, lineGaugeMaxAmount);
        lineGauge.fillAmount = lineGaugeAmount;
    }
}
