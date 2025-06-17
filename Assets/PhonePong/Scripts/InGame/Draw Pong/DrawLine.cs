// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject currentLine;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private List<Vector2> pointPosList;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 tempPointPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempPointPos, pointPosList[pointPosList.Count - 1]) > .1f)
            {
                UpdateLine(tempPointPos);
            }
        }
    }

    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider2D = currentLine.GetComponent<EdgeCollider2D>();

        pointPosList.Clear();
        pointPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        pointPosList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, pointPosList[0]);
        lineRenderer.SetPosition(1, pointPosList[1]);

        edgeCollider2D.points = pointPosList.ToArray();
    }

    private void UpdateLine(Vector2 newPointPos)
    {
        pointPosList.Add(newPointPos);

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPointPos);

        edgeCollider2D.points = pointPosList.ToArray();
    }
}
