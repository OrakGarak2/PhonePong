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

    [Header("게이지")]
    [SerializeField] private Image lineGauge;
    private const float lineGaugeMaxAmount = 1f;
    [SerializeField] private float lineGaugeAmount = lineGaugeMaxAmount;
    [SerializeField] private float lineGaugeIncreaseRate;
    [SerializeField] private float lineGaugeDecreaseRate;


    private void Start()
    {
        layerBitMask = 1 << gameObject.layer;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CheckPointInZone(out Vector2 pointPos))
        {
            CreateLine(pointPos);
        }
        else if (Input.GetMouseButton(0))
        {
            if (currentLineRacket == null) { return; }

            if (CheckPointInZone(out Vector2 nextPointPos))
            {
                if (lineGaugeAmount > 0f && Vector2.Distance(nextPointPos, pointPosList[pointPosList.Count - 1]) > nextLinePointMinDistance)
                {
                    UpdateLine(nextPointPos);
                }
            }
            else
            {
                DeleteLine();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeleteLine();
        }
        else if (lineGaugeAmount != lineGaugeMaxAmount)
        {
            UpdateLineGauge(lineGaugeAmount + lineGaugeIncreaseRate);
        }
    }

    private bool CheckPointInZone(out Vector2 pointPos)
    {
        pointPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log($"Physics2D.OverlapPoint(pointPos, thisLayerMask): {Physics2D.OverlapPoint(pointPos, layerBitMask) != null}");
        Collider2D hit = Physics2D.OverlapPoint(pointPos, layerBitMask);

        return hit != null ? hit.gameObject == gameObject : false;
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

    private void UpdateLine(Vector2 newPointPos)
    {
        UpdateLineGauge(lineGaugeAmount - lineGaugeDecreaseRate);

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
