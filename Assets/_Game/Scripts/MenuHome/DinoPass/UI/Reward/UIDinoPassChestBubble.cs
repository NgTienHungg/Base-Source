using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.DinoPass
{
    public class UIDinoPassChestBubble : MonoBehaviour
    {
        [SerializeField] private CanvasGroup bubble;
        [SerializeField] private RectTransform rewardHolder;
        [FormerlySerializedAs("miniRewardPrefab")]
        [SerializeField] private UIDinoPassReward miniDinoPassRewardPrefab;

        [Space]
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private float timeOut = 1.5f;

        [Header("Message")]
        [SerializeField] private string chestClaimed = "This reward has already been collected!";
        [SerializeField] private string vipChestNotActive = "Activate the Dino Pass to get this reward!";
        [SerializeField] private string freeChestNotUnlock = "Collect more pickaxe to unlock this stage and get this reward!";
        [SerializeField] private string vipChestNotUnlock = "Unlock this stage and activate the Dino Pass to get this reward!";
        [SerializeField] private string bonusBank = "Unlock this stage and activate the Dino Pass to get this reward!";
        [SerializeField] private string clickStage = "Collect more pickaxe to unlock this stage and get this reward!";
        [SerializeField] private string clickStageUnlock = "This stage has been unlocked! Collect more pickaxe to unlock the next stage!";

        [Header("Use for item not have script")]
        [SerializeField] private bool hideAfterClickWhenShowing;

        [Button]
        private void Replace(string oldBreakLine, string newBreakLine)
        {
            chestClaimed = chestClaimed.Replace(oldBreakLine, newBreakLine);
            vipChestNotActive = vipChestNotActive.Replace(oldBreakLine, newBreakLine);
            freeChestNotUnlock = freeChestNotUnlock.Replace(oldBreakLine, newBreakLine);
            vipChestNotUnlock = vipChestNotUnlock.Replace(oldBreakLine, newBreakLine);
            bonusBank = bonusBank.Replace(oldBreakLine, newBreakLine);
            clickStage = clickStage.Replace(oldBreakLine, newBreakLine);
        }

        private CancellationTokenSource _bubbleCts;
        private bool _isShowing;

        public bool IsShowing => _isShowing;

        public void Setup(DinoPassChest chest)
        {
            // rewardHolder.DestroyAllChildren();
            foreach (var reward in chest.ListRewards)
            {
                var miniReward = Instantiate(miniDinoPassRewardPrefab, rewardHolder);
                miniReward.gameObject.SetActive(true);
                miniReward.Setup(reward);
            }
        }

        private void OnEnable()
        {
            // this.RegisterListener(EventInGameConfig.OnShowBubbleDinoPass, HandleOnShowBubble);
        }

        private void OnDisable()
        {
            // this.RemoveListener(EventInGameConfig.OnShowBubbleDinoPass, HandleOnShowBubble);
        }

        private void HandleOnShowBubble(object sender)
        {
            if (sender != this)
            {
                Disappear();
            }
        }

        public void Hide()
        {
            _isShowing = false;

            _bubbleCts?.Cancel();
            bubble.DOKill();
            bubble.alpha = 0;
            bubble.gameObject.SetActive(false);
        }

        public void Show()
        {
            if (_isShowing && hideAfterClickWhenShowing)
            {
                Disappear();
                return;
            }

            _isShowing = true;

            // this.PostEvent(EventInGameConfig.OnShowBubbleDinoPass, this);

            bubble.DOKill();
            bubble.DOFade(1, 0.2f)
                .OnStart(() =>
                {
                    _bubbleCts?.Cancel();
                    _bubbleCts = new CancellationTokenSource();
                    bubble.gameObject.SetActive(true);
                })
                .OnComplete(async () =>
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(timeOut), cancellationToken: _bubbleCts.Token);
                    // Common.LogError("time out, hide bubble".Color("orange"));
                    Disappear();
                });
        }

        public void Disappear()
        {
            _isShowing = false;

            bubble.DOKill();
            bubble.DOFade(0f, 0.2f)
                .OnStart(() =>
                {
                    _bubbleCts?.Cancel();
                    _bubbleCts = new CancellationTokenSource();
                })
                .OnComplete(() => bubble.gameObject.SetActive(false));
        }

        public void ShowRewards()
        {
            rewardHolder.gameObject.SetActive(true);
            messageTxt.text = "";
            Show();
        }

        public void ShowMessage_ChestClaimed()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = chestClaimed;
            Show();
        }

        public void ShowMessage_VipChestNotActive()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = vipChestNotActive;
            Show();
        }

        public void ShowMessage_FreeChestNotUnlock()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = freeChestNotUnlock;
            Show();
        }

        public void ShowMessage_VipChestNotUnlock()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = vipChestNotUnlock;
            Show();
        }

        public void ShowMessage_BonusBank()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = bonusBank;
            Show();
        }

        public void ShowMessage_ClickStage()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = clickStage;
            Show();
        }

        public void ShowMessage_ClickStageUnlock()
        {
            rewardHolder.gameObject.SetActive(false);
            messageTxt.text = clickStageUnlock;
            Show();
        }
    }
}