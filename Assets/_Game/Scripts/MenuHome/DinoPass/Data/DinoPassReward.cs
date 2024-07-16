using System;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassReward
    {
        public EDinoReward RewardType;
        public int Quantity = 1;
    }
}