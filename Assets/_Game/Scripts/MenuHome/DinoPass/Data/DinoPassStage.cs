using System;
using Sirenix.OdinInspector;

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

        public DinoPassChest GetChest(EDinoPassChest chestType)
        {
            return chestType switch
            {
                EDinoPassChest.Free => FreeChest,
                EDinoPassChest.Vip => VipChest,
            };
        }
    }
}