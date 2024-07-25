using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DinoPass
{
    public enum EDinoPassChest
    {
        Free,
        Vip
    }

    public class UIDinoPassChestItem : MonoBehaviour
    {
        [SerializeField] private EDinoPassChest type;

        [Header("Reward")]
        [SerializeField] private UIDinoPassChestBubble bubble;

        [Header("Lock")]
        [SerializeField] private GameObject claimedIcon;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private GameObject claimBtn;
        [SerializeField] private Vector2 originalSize;

        [Title("Single Reward")]
        [SerializeField] private UIDinoPassReward uiSingleReward;

        [Header("Multi Rewards")]
        [SerializeField] private GameObject multiRewardsHolder;
        [SerializeField] private List<UIDinoPassReward> listRewardInMultiple;
        [SerializeField] private List<Vector3> rewardPositionsPreset_2;
        [SerializeField] private List<Vector3> rewardPositionsPreset_3;

        private DinoPassStage _dinoPassStage;
        private DinoPassDataSave _dataSave;
        private DinoPassDataConfig _dataConfig;
        private DinoPassChest _chest;

        private bool IsFree => type == EDinoPassChest.Free;

        private bool IsUnlock => _dataSave.CurrentStageId > _dinoPassStage.Id;

        private bool IsClaimed => _dataSave.IsRewardClaimed(type, _dinoPassStage.Id);

        public void Setup(DinoPassStage dinoPassStage)
        {
            _dinoPassStage = dinoPassStage;
            // _dataConfig = GameManager.Instance.DinoPassDataConfig;
            // _dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;

            uiSingleReward.gameObject.SetActive(true);
            multiRewardsHolder.SetActive(false);

            _chest = dinoPassStage.GetChest(type);
            if (_chest.IsUseChest)
            {
                uiSingleReward.SetupChest(_chest.ChestSprite);
                bubble.Setup(_chest);
            }
            else if (_chest.IsMultipleFrameOrTheme())
            {
                uiSingleReward.gameObject.SetActive(false);
                multiRewardsHolder.SetActive(true);

                var quantity = _chest.ListRewards.Count;
                var rewardPosPreset = quantity == 2 ? rewardPositionsPreset_2 : rewardPositionsPreset_3;
                listRewardInMultiple.ForEach(x => x.gameObject.SetActive(false));
                for (var i = 0; i < quantity; i++)
                {
                    listRewardInMultiple[i].gameObject.SetActive(true);
                    listRewardInMultiple[i].Setup(_chest.ListRewards[i]);
                    listRewardInMultiple[i].transform.localPosition = rewardPosPreset[i];
                }
            }
            else
            {
                uiSingleReward.Setup(_chest.FirstReward);
                uiSingleReward.rewardImg.rectTransform.sizeDelta = originalSize;
            }
        }

        public void Refresh()
        {
            claimedIcon.SetActive(false);
            lockIcon.SetActive(false);
            claimBtn.SetActive(false);

            bubble.Hide();

            #region === UI ===
            if (IsUnlock) // nhận được quà
            {
                claimedIcon.SetActive(IsClaimed);
                claimBtn.SetActive(!IsClaimed);

                if (!IsFree && !_dataSave.IsActiveVip)
                    lockIcon.SetActive(true);
            }
            else
            {
                if (!_dataSave.IsActiveVip)
                    lockIcon.SetActive(!IsFree);
            }
            #endregion
        }

        public void OnClickItem()
        {
            // SoundManager.Instance.PlayButtonSoundEx();

            if (bubble.IsShowing)
            {
                bubble.Disappear();
                return;
            }

            // var dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;
            // var chest = _dinoPassStage.GetChest(type);
            //
            // #region === Handle Click ===
            // if (IsClaimed)
            // {
            //     bubble.ShowMessage_ChestClaimed();
            //     return;
            // }
            //
            // if (chest.IsUseChest)
            // {
            //     bubble.ShowRewards();
            //     return;
            // }
            //
            // if (IsUnlock)
            // {
            //     if (!IsFree && !dataSave.IsActiveVip)
            //         bubble.ShowMessage_VipChestNotActive();
            //     else
            //         OnClickClaimButton();
            // }
            // else
            // {
            //     if (IsFree)
            //         bubble.ShowMessage_FreeChestNotUnlock();
            //     else
            //         bubble.ShowMessage_VipChestNotUnlock();
            // }
            // #endregion
        }

        public void OnClickClaimButton()
        {
            // SoundManager.Instance.PlayButtonSoundEx();

            // show purchase popup
            if (!IsFree && !_dataSave.IsActiveVip)
            {
                // UIPopUpManager.Instance.CreatePopUp<UIPopupPurchaseDinoPass>(BoatPackingConfig.UI_POPUP_PURCHASE_DINO_PASS);
                return;
            }

            ClaimReward();
        }

        private void ClaimReward()
        {
            

            _chest.Claim();
            _dataSave.ClaimReward(type, _dinoPassStage.Id);

            // TODO: effect claim reward
            claimBtn.SetActive(false);
            claimedIcon.SetActive(true);

            // TODO: frame, theme
            // ProfileManager.Instance.ListFrameUnlocked.Add(itemId);
        }

        [Button]
        private void SavePreset()
        {
            var presetId = listRewardInMultiple.Count(x => x.gameObject.activeInHierarchy);

            if (presetId != 2 && presetId != 3)
            {
                // Common.Log("Preset not found".Color("red"));
                return;
            }

            var preset = new List<Vector3>();
            for (var i = 0; i < listRewardInMultiple.Count; i++)
            {
                preset.Add(listRewardInMultiple[i].transform.localPosition);
            }

            if (presetId == 2)
                rewardPositionsPreset_2 = preset;
            else
                rewardPositionsPreset_3 = preset;

            // Common.Log("Save preset".Color("lime"));
        }
    }
}