using System;
using System.Collections.Generic;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

namespace ViewPager
{
    public class UIHomeViewPager : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [Header("Enhanced Scroller")]
        [SerializeField] private List<EnhancedScrollerCellView> listPages;
        [SerializeField] private List<UIViewPageTab> listTabs;
        [SerializeField] private RectTransform bgTabSelecting;
        
        
        [Header("Scroll Bar")]
        [SerializeField] private Scrollbar scrollbar;
        private List<float> listScrollValue;

        public static Action<int> OnChangeTab;

        private int _currentTabIndex;

        private float _scrollPos;
        private float _deltaXSwipe;
        private Vector3 _currSwipePos;
        private float _marginSwipeMax = 1.7f;

        private int NumberOfCells => listPages.Count;

        private void OnEnable()
        {
            OnChangeTab += ChangeTab;
        }

        private void OnDisable()
        {
            OnChangeTab -= ChangeTab;
        }

        private void Start()
        {
            SetTabImmediately(NumberOfCells / 2);
            // ChangeTab(NumberOfCells / 2);
        }

        private void SetTabImmediately(int index)
        {
            Debug.Log($"set tab : {index}".Color("orange"));
            _currentTabIndex = index;

            for (var i = 0; i < NumberOfCells; i++)
                listTabs[i].SetSelected(i == index);

            bgTabSelecting.position = listTabs[index].GetPos();
        }

        private void ChangeTab(int index)
        {
            Debug.Log($"change tab : {index}".Color("cyan"));

            if (!IsValidIndex(index))
                return;

            _currentTabIndex = index;

            for (var i = 0; i < NumberOfCells; i++)
                listTabs[i].SetSelected(i == index);

            // anim bg selecting
            bgTabSelecting.DOKill();
            bgTabSelecting.sizeDelta = listTabs[index].GetSizeDelta();
            bgTabSelecting.DOMoveX(listTabs[index].GetPos().x, 0.4f).SetEase(Ease.OutQuart);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _scrollPos = 0;
                if (Input.GetMouseButton(0))
                {
                    _deltaXSwipe = 0;
                    _currSwipePos.x = CameraUtils.GetMouseWorldPosition().x;
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _deltaXSwipe = CameraUtils.GetMouseWorldPosition().x - _currSwipePos.x;
                    SnapScrollView();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (Input.GetMouseButton(0))
            {
                Gizmos.color = Color.red;
                Vector3 startPos = CameraUtils.GetMouseWorldPosition();
                Vector3 endPos = new Vector3(_currSwipePos.x, startPos.y, startPos.z);
                Gizmos.DrawLine(startPos, endPos);
            }
        }

        private void SnapScrollView()
        {
            if (Mathf.Abs(_deltaXSwipe) > _marginSwipeMax)
            {
                if (_deltaXSwipe > 0)
                    _currentTabIndex = Mathf.Clamp(_currentTabIndex--, 0, NumberOfCells - 1);
                else
                    _currentTabIndex = Mathf.Clamp(_currentTabIndex++, 0, NumberOfCells - 1);
            }

            ChangeTab(_currentTabIndex);
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < NumberOfCells;
        }

        #region === Enhanced Scroller ===
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return NumberOfCells;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 1080;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            if (!IsValidIndex(dataIndex))
                return null;

            listPages[dataIndex].gameObject.SetActive(true);
            return listPages[dataIndex];
        }
        #endregion
    }
}