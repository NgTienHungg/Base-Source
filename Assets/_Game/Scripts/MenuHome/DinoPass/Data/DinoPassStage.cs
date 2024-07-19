using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassStage
    {
        [ReadOnly]
        public int Id;

        public int PickaxeCount;

        [TabGroup("Free")] [HideLabel]
        public DinoPassChest FreeChest;

        [TabGroup("VIP")] [HideLabel]
        public DinoPassChest VipChest;

        // public DinoPassChest GetChest(EDinoPassChest chestType)
        // {
        //     return chestType switch
        //     {
        //         EDinoPassChest.Free => FreeChest,
        //         EDinoPassChest.Vip => VipChest,
        //     };
        // }
    }

    [Serializable]
    public class DinoPassChest
    {
        [ShowIf("@HasMultiReward()")]
        [PreviewField(50)]
        public Sprite ChestSprite;

        public List<DinoPassReward> ListRewards;

        public DinoPassReward FirstReward => ListRewards[0];

        public bool HasMultiReward()
        {
            return ListRewards.Count > 1;
        }
    }
}