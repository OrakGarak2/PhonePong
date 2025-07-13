// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

public class DrawLineZone : TouchZone
{
    [Header("라인 패들")]
    [SerializeField] private GameObject linePaddlePrefab;
    [SerializeField] private GameObject currentLinePaddle;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private List<Vector2> pointPosList;

    private const float nextLinePointMinDistance = 0.15f;

    [Header("게이지")]
    [SerializeField] private Image lineGauge;
    private const float lineGaugeMaxAmount = 1f;
    [SerializeField] private float lineGaugeAmount = lineGaugeMaxAmount;
    [SerializeField] private float lineGaugeIncreaseRate;
    private const float lineMaxDistance = 4f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (CheckPointInZone(out Vector2 pointPos))
        {
            if (currentLinePaddle == null)
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

    private void CreateLine(Vector2 pointPos)
    {
        currentLinePaddle = Instantiate(linePaddlePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLinePaddle.GetComponent<LineRenderer>();
        edgeCollider2D = currentLinePaddle.GetComponent<EdgeCollider2D>();

        pointPosList.Clear();
        pointPosList.Add(pointPos);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, pointPosList[0]);

        edgeCollider2D.points = pointPosList.ToArray();

        edgeCollider2D.enabled = true;

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.lineDraw, pointPos);
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
        if (currentLinePaddle != null)
        {
            Destroy(currentLinePaddle);
        }
    }

    private void UpdateLineGauge(float newAmount)
    {
        lineGaugeAmount = Mathf.Clamp(newAmount, 0f, lineGaugeMaxAmount);
        lineGauge.fillAmount = lineGaugeAmount;
    }
}
