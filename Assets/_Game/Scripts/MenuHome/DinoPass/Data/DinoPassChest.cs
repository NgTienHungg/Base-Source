using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassChest
    {
        public bool IsUseChest;

        [ShowIf("@IsUseChest")]
        [PreviewField(50)]
        public Sprite ChestSprite;

        public List<DinoPassReward> ListRewards;

        public DinoPassReward FirstReward => ListRewards[0];

        public bool IsMultipleFrameOrTheme()
        {
            return ListRewards.Count(x => x.RewardType is EDinoReward.Frame or EDinoReward.Theme) > 1;
        }

        public void Claim()
        {
            // foreach (var reward in ListRewards)
            // {
            //     switch (reward.RewardType)
            //     {
            //         case EDinoReward.BoosterAddSlot:
            //             ProfileManager.Instance.AddSlotCount += reward.Quantity;
            //             break;
            //         case EDinoReward.BoosterShuffle:
            //             ProfileManager.Instance.ShuffleCount += reward.Quantity;
            //             break;
            //         case EDinoReward.BoosterRemove:
            //             ProfileManager.Instance.RemoveBoatCount += reward.Quantity;
            //             break;
            //         case EDinoReward.BoosterRevival:
            //             ProfileManager.Instance.ReviveCount += reward.Quantity;
            //             break;
            //         case EDinoReward.BoosterAddTime:
            //             ProfileManager.Instance.MoreTimeCount += reward.Quantity;
            //             break;
            //         case EDinoReward.Coin:
            //             ProfileManager.Instance.CurrencyCoinEarn(reward.Quantity, TrackingEventParameterValue.MOVIE_FESTIVAL);
            //             break;
            //         case EDinoReward.Theme:
            //             ProfileManager.Instance.ListFrameUnlocked.Add(reward.SkinId);
            //             break;
            //         case EDinoReward.Frame:
            //             ProfileManager.Instance.ListFrameUnlocked.Add(reward.SkinId);
            //             break;
            //         case EDinoReward.UnlimitedHeart:
            //             var unlimitedHeartEndTime = ProfileManager.Instance.UnlimitedHeartEndTime;
            //             ProfileManager.Instance.AddUnlimitedHeart(reward.Quantity);
            //             break;
            //     }
            //
            //     // TrackingManager.Instance.LogBoosterEarn(reward., reward.Quantity,
            //     //     TrackingEventParameterValue.DINO_PASS);
            //
            //     Common.LogError($"Receive {reward.RewardType} : {reward.Quantity}");
            // }
        }
    }
}