// using TMPro;
// using UnityEngine;
// using WingsMob.Scripts.Utils.Time;
//
// namespace WingsMob.BoatPacking.DinoPass
// {
//     public class UIPopupPurchaseDinoPass : UIPopUp
//     {
//         [SerializeField] private TextMeshProUGUI timeLeftTxt;
//         [SerializeField] private IAPButton iapButton;
//
//         protected override void Start()
//         {
//             base.Start();
//             iapButton.Setup(OnPurchaseSuccess, null);
//
//             StartCoroutine(TimeUtils.IECountDown(
//                 endTime: ProfileManager.Instance.UserData.DinoPassDataSave.EndTime.ToDateTime(),
//                 onUpdate: timeLeft => timeLeftTxt.text = timeLeft.Format(2),
//                 onComplete: OnEnable
//             ));
//         }
//
//         private void OnPurchaseSuccess()
//         {
//             ProfileManager.Instance.UserData.DinoPassDataSave.ActivateDinoPass();
//         }
//     }
// }