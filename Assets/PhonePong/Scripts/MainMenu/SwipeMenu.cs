using System;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    private float scrollPos = 0;
    private float[] poses;
    private Button[] buttons;
    
    [SerializeField] private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
    }

    private void Start()
    {
        buttons = new Button[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
        }
    }

    private void Update()
    {
        poses = new float[transform.childCount];
        
        float distance = 1f / (poses.Length - 1f);
        for (int i = 0; i < poses.Length; i++)
        {
            poses[i] = distance * i;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.value;
        }
        else
        {
            foreach (float t in poses)
            {
                if (scrollPos < t + (distance / 2) && scrollPos > t - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, t, 0.1f);
                }
            }
        }

        for (int i = 0; i < poses.Length; i++)
        {
            if (scrollPos < poses[i] + (distance / 2) && scrollPos > poses[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                buttons[i].interactable = true;
                for (int j = 0; j < poses.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        buttons[j].interactable = false;
                    }
                }
            }
        }
    }
}
