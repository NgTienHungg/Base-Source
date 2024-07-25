using Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DinoPass
{
    public class UIDinoPassReward : MonoBehaviour
    {
        public Image rewardImg;
        public TextMeshProUGUI quantityTxt;

        [Header("Profile")]
        public GameObject profileObj;
        public Image frameImg;
        public Image themeImg;

        public void Setup(DinoPassReward reward)
        {
            var dataConfig = DataManager.DataConfig.DinoPass;

            // pre init
            rewardImg.enabled = true;
            profileObj.SetActive(false);

            // set single reward
            rewardImg.sprite = dataConfig.GetRewardSprite(reward.RewardType);

            if (reward.RewardType is EDinoReward.UnlimitedHeart)
                quantityTxt.text = reward.Quantity + "m";
            else
                quantityTxt.text = (reward.RewardType != EDinoReward.Coin ? "x" : "") + reward.Quantity;

            // frame | theme
            if (reward.RewardType is EDinoReward.Frame)
            {
                rewardImg.enabled = false;
                profileObj.SetActive(true);
                frameImg.gameObject.SetActive(true);
                themeImg.gameObject.SetActive(false);
                // frameImg.sprite = GameManager.Instance.AvatarData.Frames[reward.SkinId].Icon;
            }
            else if (reward.RewardType is EDinoReward.Theme)
            {
                rewardImg.enabled = false;
                profileObj.SetActive(true);
                frameImg.gameObject.SetActive(true);
                themeImg.gameObject.SetActive(true);
                // frameImg.sprite = GameManager.Instance.AvatarData.Frames[0].Icon;
                // themeImg.sprite = GameManager.Instance.AvatarData.Effect[reward.SkinId].Icon;
            }
        }

        public void SetupChest(Sprite chestSprite)
        {
            // pre init
            rewardImg.enabled = true;
            profileObj.SetActive(false);

            rewardImg.sprite = chestSprite;
            rewardImg.SetNativeSize();
            quantityTxt.text = "";
        }
    }
}