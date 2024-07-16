using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DinoPass
{
    public enum EDinoPassStage
    {
        Unlock,
        Current,
        Lock
    }

    public class UIDinoPassRewardStage : EnhancedScrollerCellView
    {
        [Header("Reward Item")]
        [SerializeField] private UIDinoPassRewardItem freeRewardItem;
        [SerializeField] private UIDinoPassRewardItem vipRewardItem;

        [Title("Progress Bar")]
        [SerializeField] private GameObject activeFrame;
        [SerializeField] private Image levelIcon;
        [SerializeField] private Sprite unlockStageIconSprite;
        [SerializeField] private Sprite lockStageIconSprite;
        [SerializeField] private Image progressFill;
        [SerializeField] private TextMeshProUGUI stageTxt;

        private DinoPassStage _dinoPassStage;

        public void Setup(DinoPassStage dinoPassStage)
        {
            _dinoPassStage = dinoPassStage;
            freeRewardItem.Setup(dinoPassStage);
            vipRewardItem.Setup(dinoPassStage);
        }

        public void Refresh(DinoPassDataSave dinoPassDataSave)
        {
            // check status
            var status = EDinoPassStage.Lock;
            if (dinoPassDataSave.CurrentStageId > _dinoPassStage.Id)
                status = EDinoPassStage.Unlock;
            else if (dinoPassDataSave.CurrentStageId == _dinoPassStage.Id)
                status = EDinoPassStage.Current;

            // BG & Frame
            activeFrame.SetActive(status == EDinoPassStage.Current);
            levelIcon.sprite = (status == EDinoPassStage.Unlock ? unlockStageIconSprite : lockStageIconSprite);
            stageTxt.text = (_dinoPassStage.Id + 1).ToString();

            if (status == EDinoPassStage.Unlock)
                progressFill.fillAmount = 1f;
            else if (status == EDinoPassStage.Current)
                progressFill.fillAmount = 1f * dinoPassDataSave.CurrentPickaxes / _dinoPassStage.PickaxeCount;
            else
                progressFill.fillAmount = 0f;

            // refresh item
            freeRewardItem.Refresh(dinoPassDataSave);
            vipRewardItem.Refresh(dinoPassDataSave);
        }
    }
}