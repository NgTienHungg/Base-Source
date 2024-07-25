using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.DinoPass
{
    public class UIPopupDinoPass : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [Title("Scroll View")]
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private float itemHeaderSize, itemSize;

        [Space]
        [SerializeField] private TextMeshProUGUI timeLeftTxt;
        [SerializeField] private UIPickaxeProgressBar pickaxeProgressBar;

        [Title("Item")]
        [SerializeField] private UIDinoPassRewardStage dinoPassStagePrefab;
        [SerializeField] private UIDinoPassRewardHeader dinoPassHeaderPrefab;

        [Header("Activate")]
        [SerializeField] private GameObject activateButton;

        private DinoPassDataConfig _dataConfig;
        private DinoPassDataSave _dataSave;

        private List<object> _scrollData;

        private void Awake()
        {
            scroller.Delegate = this;
            // _dataConfig = GameManager.Instance.DinoPassDataConfig;
            // _dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;

            // add data for scroll view
            _scrollData = new List<object>();
            _scrollData.Add(new DinoPassRewardHeaderData());
            _scrollData.AddRange(_dataConfig.Stages);
        }

        public void OnEnable()
        {
            CheckEndTime();
            HandlePickaxes();

            // StartCoroutine(TimeUtils.IECountDown(
            //     endTime: _dataSave.EndTime.ToDateTime(),
            //     onUpdate: timeLeft => timeLeftTxt.text = timeLeft.Format(2),
            //     onComplete: OnEnable
            // ));
        }

        private void CheckEndTime()
        {
            if (_dataSave.EndTime.ToDateTime() <= DateTime.Now)
            {
                _dataSave.Reset();
                // this.PostEvent(EventInGameConfig.OnResetDinoPass);
            }
        }

        private void HandlePickaxes()
        {
            // khi chưa unlock hết stage && đủ pickaxe để unlock stage tiếp theo
            while (_dataSave.CurrentStageId < _dataConfig.StageCount &&
                   _dataSave.CurrentPickaxes >= _dataConfig.GetPickaxeRequiredAtStage(_dataSave.CurrentStageId))
            {
                _dataSave.CurrentPickaxes -= _dataConfig.GetPickaxeRequiredAtStage(_dataSave.CurrentStageId);
                _dataSave.CurrentStageId++;
            }

            // ProfileManager.Instance.SaveLocalUserData();
        }

        public void OnClickActivateButton()
        {
            // SoundManager.Instance.PlayButtonSoundEx();
            // UIPopUpManager.Instance.CreatePopUp<UIPopupPurchaseDinoPass>(BoatPackingConfig.UI_POPUP_PURCHASE_DINO_PASS);
        }

        public void OnClickHelpButton()
        {
            // SoundManager.Instance.PlayButtonSoundEx();
            // UIPopUpManager.Instance.CreatePopUp(BoatPackingConfig.UI_POPUP_TUTORIAL_DINO_PASS);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _dataConfig.StageCount + 1;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return dataIndex == 0 ? itemHeaderSize : itemSize;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            if (_scrollData[dataIndex] is DinoPassRewardHeaderData)
            {
                var cellView = scroller.GetCellView(dinoPassHeaderPrefab) as UIDinoPassRewardHeader;
                cellView.name = "[Header]";
                return cellView;
            }

            if (_scrollData[dataIndex] is DinoPassStage)
            {
                var cellView = scroller.GetCellView(dinoPassStagePrefab) as UIDinoPassRewardStage;
                var dinoPassStage = _scrollData[dataIndex] as DinoPassStage;
                cellView!.name = $"[Stage] {dinoPassStage.Id + 1}";
                cellView.Setup(dinoPassStage);
                cellView.Refresh();
                return cellView;
            }

            return null;
        }
    }
}