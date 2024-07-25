// using TMPro;
// using UnityEngine;
//
// namespace Game.DinoPass
// {
//     public class UIPopupPurchaseDinoPass : UIPopUp
//     {
//         [SerializeField] private TextMeshProUGUI timeLeftTxt;
//         [SerializeField] private IAPButton iapButton;
//
//         protected override void Start()
//         {
//             base.Start();
//             // iapButton.Setup(OnPurchaseSuccess, null);
//
//             StartCoroutine(TimeUtils.IECountDown(
//                 endTime: ProfileManager.Instance.UserData.DinoPassDataSave.EndTime.ToDateTime(),
//                 onUpdate: timeLeft => timeLeftTxt.text = timeLeft.Format(2),
//                 onComplete: OnEnable
//             ));
//         }
//
//         public void OnPurchaseSuccess()
//         {
//             Common.LogError("Active Dino Pass".Color("lime"));
//             ProfileManager.Instance.UserData.DinoPassDataSave.ActivateDinoPass();
//             this.PostEvent(EventInGameConfig.OnActivateDinoPass);
//             ClosePopUp();
//         }
//     }
// }