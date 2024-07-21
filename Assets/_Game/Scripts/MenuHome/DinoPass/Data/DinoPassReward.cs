using System;
using Sirenix.OdinInspector;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassReward
    {
        public EDinoReward RewardType;
        public int Quantity = 1;

        [ShowIf("@IsFrameOrTheme()")] [GUIColor("yellow")]
        public int SkinId;

        public bool IsFrameOrTheme()
        {
            return RewardType is EDinoReward.Frame or EDinoReward.Theme;
        }

        public bool IsUnlimitedHeart()
        {
            return RewardType == EDinoReward.UnlimitedHeart;
        }
    }
}