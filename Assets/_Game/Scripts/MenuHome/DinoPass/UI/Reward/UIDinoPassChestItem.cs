// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace WingsMob.BoatPacking.DinoPass
// {
//     public enum EDinoPassChest
//     {
//         Free,
//         Vip
//     }
//
//     public class UIDinoPassChestItem : MonoBehaviour
//     {
//         [SerializeField] private EDinoPassChest type;
//
//         [Header("Reward")]
//         [SerializeField] private UIReward rewardItem;
//         [SerializeField] private UIDinoPassChestBubble bubble;
//
//         [Header("Lock")]
//         [SerializeField] private GameObject claimedIcon;
//         [SerializeField] private GameObject lockIcon;
//         [SerializeField] private GameObject claimBtn;
//         [SerializeField] private Vector2 originalSize;
//
//         private DinoPassStage _dinoPassStage;
//         private DinoPassDataSave _dataSave;
//
//         public void Setup(DinoPassStage dinoPassStage)
//         {
//             _dinoPassStage = dinoPassStage;
//             _dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;
//
//             var dataConfig = GameManager.Instance.DinoPassDataConfig;
//             var chest = dinoPassStage.GetChest(type);
//
//             if (chest.HasMultiReward())
//             {
//                 rewardItem.rewardImg.sprite = chest.ChestSprite;
//                 rewardItem.rewardImg.SetNativeSize();
//                 rewardItem.quantityTxt.text = "";
//
//                 bubble.Setup(chest);
//             }
//             else
//             {
//                 rewardItem.rewardImg.sprite = dataConfig.GetRewardSprite(chest.FirstReward.RewardType);
//                 rewardItem.rewardImg.rectTransform.sizeDelta = originalSize;
//                 rewardItem.quantityTxt.text = "x" + chest.FirstReward.Quantity;
//             }
//         }
//
//         public void Refresh()
//         {
//             claimedIcon.SetActive(false);
//             lockIcon.SetActive(false);
//             claimBtn.SetActive(false);
//
//             bubble.Hide();
//
//             #region === UI ===
//             var isUnlock = _dataSave.CurrentStageId > _dinoPassStage.Id;
//             var isFree = type == EDinoPassChest.Free;
//
//             if (isUnlock) // nhận được quà
//             {
//                 if (isFree) // miễn phí
//                 {
//                     var isClaimed = _dataSave.FreeRewardsClaimed.Contains(_dinoPassStage.Id);
//                     claimedIcon.SetActive(isClaimed);
//                     claimBtn.SetActive(!isClaimed);
//                 }
//                 else
//                 {
//                     if (_dataSave.IsActiveVip) // đã mua Dino Pass
//                     {
//                         var isClaimed = _dataSave.VipRewardsClaimed.Contains(_dinoPassStage.Id);
//                         claimedIcon.SetActive(isClaimed);
//                         claimBtn.SetActive(!isClaimed);
//                     }
//                     else
//                     {
//                         lockIcon.SetActive(true);
//                     }
//                 }
//             }
//             else
//             {
//                 // only set lock icon for VIP chest
//                 lockIcon.SetActive(!isFree);
//             }
//             #endregion
//         }
//
//         public void OnClickItem()
//         {
//             SoundManager.Instance.PlayButtonSoundEx();
//
//             if (bubble.IsShowing)
//             {
//                 bubble.Disappear();
//                 return;
//             }
//
//             var dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;
//             var chest = _dinoPassStage.GetChest(type);
//
//             var isUnlock = dataSave.CurrentStageId > _dinoPassStage.Id;
//             var isFree = type == EDinoPassChest.Free;
//             var isClaimed = isFree
//                 ? dataSave.FreeRewardsClaimed.Contains(_dinoPassStage.Id)
//                 : dataSave.VipRewardsClaimed.Contains(_dinoPassStage.Id);
//
//             #region === Handle Click ===
//             if (isClaimed)
//             {
//                 bubble.ShowMessage_ChestClaimed();
//                 return;
//             }
//
//             if (chest.HasMultiReward())
//             {
//                 bubble.ShowRewards();
//                 return;
//             }
//
//             if (isUnlock)
//             {
//                 if (!isFree && !dataSave.IsActiveVip)
//                     bubble.ShowMessage_VipChestNotActive();
//                 else
//                     OnClickClaimButton();
//             }
//             else
//             {
//                 if (isFree)
//                     bubble.ShowMessage_FreeChestNotUnlock();
//                 else
//                     bubble.ShowMessage_VipChestNotUnlock();
//             }
//             #endregion
//         }
//
//         public void OnClickClaimButton()
//         {
//             // Common.LogError("Claim reward".Color("red"));
//             SoundManager.Instance.PlayButtonSoundEx();
//             _dataSave.ClaimReward(type, _dinoPassStage.Id);
//
//             // TODO: effect claim reward
//             claimBtn.SetActive(false);
//             claimedIcon.SetActive(true);
//
//             // TODO: frame, theme
//             // ProfileManager.Instance.ListFrameUnlocked.Add(itemId);
//         }
//     }
// }