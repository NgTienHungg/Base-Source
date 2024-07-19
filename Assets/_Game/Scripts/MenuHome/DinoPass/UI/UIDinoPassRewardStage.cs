// using EnhancedUI.EnhancedScroller;
// using Sirenix.OdinInspector;
// using TMPro;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
//
// namespace WingsMob.BoatPacking.DinoPass
// {
//     public enum EDinoPassStage
//     {
//         Unlock,
//         Current,
//         Lock
//     }
//
//     public class UIDinoPassRewardStage : EnhancedScrollerCellView
//     {
//         [FormerlySerializedAs("freeRewardItem")]
//         [Header("Reward Item")]
//         [SerializeField] private UIDinoPassChestItem freeChestItem;
//         [FormerlySerializedAs("vipRewardItem")]
//         [SerializeField] private UIDinoPassChestItem vipChestItem;
//
//         [Title("Progress Bar")]
//         [SerializeField] private GameObject activeFrame;
//         [SerializeField] private Image levelIcon;
//         [SerializeField] private Sprite unlockStageIconSprite;
//         [SerializeField] private Sprite lockStageIconSprite;
//         [SerializeField] private Image progressFill;
//         [SerializeField] private TextMeshProUGUI stageTxt;
//
//         [Space]
//         [SerializeField] private UIDinoPassChestBubble stageBubble;
//
//         private DinoPassStage _dinoPassStage;
//         private DinoPassDataSave _dataSave;
//
//         public void Setup(DinoPassStage dinoPassStage)
//         {
//             _dinoPassStage = dinoPassStage;
//             _dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;
//
//             freeChestItem.Setup(dinoPassStage);
//             vipChestItem.Setup(dinoPassStage);
//
//             stageBubble.Hide();
//         }
//
//         public void Refresh(DinoPassDataSave dinoPassDataSave)
//         {
//             // check status
//             var status = EDinoPassStage.Lock;
//             if (dinoPassDataSave.CurrentStageId > _dinoPassStage.Id)
//                 status = EDinoPassStage.Unlock;
//             else if (dinoPassDataSave.CurrentStageId == _dinoPassStage.Id)
//                 status = EDinoPassStage.Current;
//
//             // BG & Frame
//             activeFrame.SetActive(status == EDinoPassStage.Current);
//             levelIcon.sprite = (status == EDinoPassStage.Unlock ? unlockStageIconSprite : lockStageIconSprite);
//             stageTxt.text = (_dinoPassStage.Id + 1).ToString();
//
//             if (status == EDinoPassStage.Unlock)
//                 progressFill.fillAmount = 1f;
//             else if (status == EDinoPassStage.Current)
//                 progressFill.fillAmount = 1f * dinoPassDataSave.CurrentPickaxes / _dinoPassStage.PickaxeCount;
//             else
//                 progressFill.fillAmount = 0f;
//
//             // refresh item
//             freeChestItem.Refresh();
//             vipChestItem.Refresh();
//         }
//
//         public void OnClickStageIcon()
//         {
//             SoundManager.Instance.PlayButtonSoundEx();
//
//             if (_dataSave.IsUnlockStage(_dinoPassStage.Id))
//                 stageBubble.ShowMessage_ClickStageUnlock();
//             else
//                 stageBubble.ShowMessage_ClickStage();
//         }
//     }
// }