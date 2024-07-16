using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassStage
    {
        [ReadOnly]
        public int Id;

        public int PickaxeCount;

        [TabGroup("Free")]
        public List<DinoPassReward> FreeRewards;

        [TabGroup("VIP")]
        public List<DinoPassReward> PassRewards;
    }
}