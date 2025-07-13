// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace PhonePong.VSRetro.Tooltip
{
    public class TooltipUser
    {
        private Vector2 toolTipPos;
        private string tooltipContentName;
        private string tooltipContentDesc;

        public TooltipUser()
        {

        }

        public TooltipUser(Vector2 toolTipPos, string tooltipContentName, string tooltipContentDesc)
        {
            this.toolTipPos = toolTipPos;
            this.tooltipContentName = tooltipContentName;
            this.tooltipContentDesc = tooltipContentDesc;
        }

        /// <summary>
        /// 기존에 입력되어 있던 정보를 바탕으로 툴팁을 표시하는 메서드
        /// </summary>
        public void ShowTooltip()
        {
            Tooltip.Instance.ShowTooltip(toolTipPos, tooltipContentName, tooltipContentDesc);
        }

        /// <summary>
        /// 매개변수로 들어온 정보를 바탕으로 툴팁을 표시하는 메서드
        /// </summary>
        /// <param name="pos">툴팁의 위치</param>
        /// <param name="name">툴팁의 내용물의 이름</param>
        /// <param name="desc">툴팁의 내용물의 설명</param>
        /// <param name="isPersistent">이 정보의 지속적인 사용 여부</param>
        public void ShowTooltip(Vector2 pos, string name, string desc, bool isPersistent = true)
        {
            if (isPersistent)
            {
                SetTooltip(pos, name, desc);
            }

            Tooltip.Instance.ShowTooltip(pos, name, desc);
        }

        public void HideTooltip()
        {
            Tooltip.Instance.HideTooltip();
        }

        public void SetTooltip(Vector2 pos, string name, string desc)
        {
            toolTipPos = pos;
            tooltipContentName = name;
            tooltipContentDesc = desc;
        }
    }
}