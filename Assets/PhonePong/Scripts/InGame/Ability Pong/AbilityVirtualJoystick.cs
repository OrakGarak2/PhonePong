// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// PhonePong
using PhonePong.Enum;

public class AbilityVirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("능력: UI")]
    [SerializeField] GameObject abilityUI;
    [SerializeField] RectTransform[] abilityUIArray;
    [SerializeField] Image[] abilityPanelImageArray;
    [SerializeField] Color selectedColor;
    [SerializeField] Color originalColor;

    [Header("능력: 값")]
    [SerializeField] Vector2[] abilityUIDirectionArray;
    [SerializeField] float abilityCooldown;
    [SerializeField] bool canUseAbility = true;
    [SerializeField] int selectedAbilityIndex = notSelectedAbilityIndex;
    private const int notSelectedAbilityIndex = -1;

    [Header("가상 조이스틱")]
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RectTransform lever;
    [SerializeField] Image leverImage;
    [SerializeField] private float leverRange;

    [SerializeField] private PlayerEnum playerEnum;
    private float joystickToPanelDirectionX{
        get
        {
            if (playerEnum == PlayerEnum.Player1)   return -1;
            else                                    return 1;
        }
    }

    [Header("패들")]
    [SerializeField] private AbilityPaddle paddle;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        leverImage = lever.GetComponent<Image>();

        abilityPanelImageArray = new Image[abilityUIArray.Length];
        abilityUIDirectionArray = new Vector2[abilityUIArray.Length];

        for (int i = 0; i < abilityUIArray.Length; i++)
        {
            abilityPanelImageArray[i] = abilityUIArray[i].GetComponent<Image>();
            abilityUIDirectionArray[i] = ((Vector2)(abilityUIArray[i].position - rectTransform.position)).normalized;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canUseAbility) { return; }

        abilityUI.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canUseAbility) { return; }

        lever.anchoredPosition = GetLeverPos(eventData.position, rectTransform.position);

        Vector2 leverDirection = ((Vector2)(lever.position - rectTransform.position)).normalized;

        if (CheckInAbilityPanel(leverDirection))
        {
            float approximateValue = 0f;
            for (int i = 0; i < abilityUIDirectionArray.Length; i++)
            {
                float dotProductValue = leverDirection.x * abilityUIDirectionArray[i].x + leverDirection.y * abilityUIDirectionArray[i].y;
                if (dotProductValue > approximateValue)
                {
                    ChangeSelectedAbility(i);
                    approximateValue = dotProductValue;
                }
            }
        }
        else if (selectedAbilityIndex != notSelectedAbilityIndex)
        {
            abilityPanelImageArray[selectedAbilityIndex].color = originalColor;
            selectedAbilityIndex = notSelectedAbilityIndex;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        abilityUI.SetActive(false);
        lever.anchoredPosition = Vector2.zero;
        
        if (selectedAbilityIndex >= 0)
        {
            abilityUIArray[selectedAbilityIndex].GetComponent<Ability>().Excute(paddle);
            leverImage.fillAmount = 0f;
            canUseAbility = false;

            abilityPanelImageArray[selectedAbilityIndex].color = originalColor;
            selectedAbilityIndex = notSelectedAbilityIndex;

            StartCoroutine(CoroutineUpdateCooldown());
        }
    }

    private Vector2 GetLeverPos(Vector2 eventPosition, Vector2 joystickPos)
    {
        Vector2 newLeverPos = eventPosition - joystickPos;
        newLeverPos = Math.Pow(newLeverPos.x, 2) + Math.Pow(newLeverPos.y, 2) <= Math.Pow(leverRange, 2)
        ? newLeverPos : newLeverPos.normalized * leverRange;

        return newLeverPos;
    }

    private bool CheckInAbilityPanel(Vector2 leverDirection)
    {
        // 능력 패널이 조이스틱 옆에 반원으로 나타나기 때문에
        // 조이스틱과 레버의 방향 벡터와 조이스틱과 패널의 방향 벡터(x값만 계산하는 이유는 조이스틱과 패널의 방향 벡터의 y값이 0이기 때문)를
        // 내적하여 현재 조이스틱이 어빌리티 패널 안에 있는지를 확인한다.
        return leverDirection.x * joystickToPanelDirectionX >= 0;
    }

    private void ChangeSelectedAbility(int index)
    {
        if (selectedAbilityIndex == index) { return; }

        if (selectedAbilityIndex != notSelectedAbilityIndex)
        {
            abilityPanelImageArray[selectedAbilityIndex].color = originalColor;
        }

        originalColor = abilityPanelImageArray[selectedAbilityIndex = index].color;
        abilityPanelImageArray[selectedAbilityIndex].color = selectedColor;
    }

    private IEnumerator CoroutineUpdateCooldown()
    {
        float timer = 0f;

        while (timer < abilityCooldown)
        {
            timer += Time.deltaTime;
            leverImage.fillAmount = timer / abilityCooldown;

            yield return null;
        }

        leverImage.fillAmount = 1f;
        canUseAbility = true;
    }
}
