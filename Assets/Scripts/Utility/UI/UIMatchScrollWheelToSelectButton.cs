using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility.UIs
{
    public class UIMatchScrollWheelToSelectButton : MonoBehaviour
    {
        /// 该类用于自动将滚动视图滚动到当前选中的按钮。
        // Start is called before the first frame update
        /// 当前选中的游戏对象。
        [SerializeField]private GameObject currentSelected;
        /// 上一次选中的游戏对象。
        [SerializeField]private GameObject previousSelected;
        [SerializeField]private RectTransform currentSelectedRectTransform;
        /// 滚动面板内容区域的 RectTransform 组件。
        [SerializeField]private RectTransform contentPanel;
        /// 控制滚动面板滚动的 ScrollRect 组件。
        [SerializeField]private ScrollRect scrollRect;


        public void Start()
        {
            // 设置Content的大小
            contentPanel.sizeDelta = new Vector2(0, 1000);

            // 设置滚动视图的滚动范围
            scrollRect.verticalNormalizedPosition = 1;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
        }

        private void Update()
        {
            currentSelected = EventSystem.current.currentSelectedGameObject;
            if (currentSelected)
            {
                if (currentSelected != previousSelected)
                {
                    previousSelected = currentSelected;
                    currentSelectedRectTransform = currentSelected.GetComponent<RectTransform>();
                    SnapTo(currentSelectedRectTransform);
                }
            }
        }

        private void SnapTo(RectTransform target)
        {
            // 强制更新所有 Canvas，确保布局和渲染正确
            Canvas.ForceUpdateCanvases();
            // 计算新的位置，使目标位于滚动面板的中心
            Vector2 newPosition = 
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)-(Vector2)scrollRect.transform.InverseTransformPoint(target.position);
            
            newPosition.x = 0;
            contentPanel.anchoredPosition = newPosition;
        }
    }
}
