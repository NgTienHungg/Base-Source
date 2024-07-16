using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DinoPass
{
    public class UIDinoPassRewardItem : MonoBehaviour
    {
        public enum EDinoPassRewardItemType
        {
            Free,
            Vip
        }

        [SerializeField] private EDinoPassRewardItemType type;

        [Header("Reward")]
        [SerializeField] private Image rewardImg;
        [SerializeField] private TextMeshProUGUI quantityTxt;

        [Header("Lock")]
        [SerializeField] private GameObject claimedIcon;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private GameObject claimBtn;

        private DinoPassStage _dinoPassStage;

        public void Setup(DinoPassStage dinoPassStage)
        {
            _dinoPassStage = dinoPassStage;
        }

        public void Refresh(DinoPassDataSave dataSave)
        {
            claimedIcon.SetActive(false);
            lockIcon.SetActive(false);
            claimBtn.SetActive(false);

            var isUnlock = dataSave.CurrentStageId > _dinoPassStage.Id;
            var isFree = type == EDinoPassRewardItemType.Free;

            if (isUnlock) // nhận được quà
            {
                if (isFree) // miễn phí
                {
                    var isClaimed = dataSave.FreeRewardsClaimed.Contains(_dinoPassStage.Id);
                    claimedIcon.SetActive(isClaimed);
                    claimBtn.SetActive(!isClaimed);
                }
                else
                {
                    if (dataSave.IsActiveVip) // đã mua Dino Pass
                    {
                        var isClaimed = dataSave.VipRewardsClaimed.Contains(_dinoPassStage.Id);
                        claimedIcon.SetActive(isClaimed);
                        claimBtn.SetActive(!isClaimed);
                    }
                    else
                    {
                        lockIcon.SetActive(true);
                    }
                }
            }
            else
            {
                lockIcon.SetActive(true);
            }
        }

        public void OnClickClaimButton()
        {
            Debug.LogError("Claim reward".Color("red"));
        }
    }
}