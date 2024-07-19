// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace WingsMob.BoatPacking.DinoPass
// {
//     public class UIReward : MonoBehaviour
//     {
//         public Image rewardImg;
//         public TextMeshProUGUI quantityTxt;
//
//         public void Setup(DinoPassReward reward)
//         {
//             var dataConfig = GameManager.Instance.DinoPassDataConfig;
//             rewardImg.sprite = dataConfig.GetRewardSprite(reward.RewardType);
//             quantityTxt.text = (reward.RewardType != EDinoReward.Coin ? "x" : "") + reward.Quantity;
//         }
//     }
// }