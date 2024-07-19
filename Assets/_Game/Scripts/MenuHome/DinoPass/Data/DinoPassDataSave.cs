using System;
using System.Collections.Generic;
using WingsMob.BoatPacking;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassDataSave
    {
        public bool IsActiveVip;

        public int CurrentStageId;
        public int CurrentPickaxes;

        public SerializableDateTime EndTime = new SerializableDateTime();

        public List<int> FreeRewardsClaimed = new List<int>();
        public List<int> VipRewardsClaimed = new List<int>();

        public bool IsUnlockStage(int stageId)
        {
            return CurrentStageId > stageId;
        }

        public void Reset()
        {
            Logger.LogError("Reset Dino Pass".Color("yellow"));

            IsActiveVip = false;

            CurrentStageId = 0;
            CurrentPickaxes = 0;

            FreeRewardsClaimed.Clear();
            VipRewardsClaimed.Clear();

            // Reset end time to the beginning of the month
            var newEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).AddMonths(1);
            EndTime = new SerializableDateTime(newEndTime);
        }

        public void ActivateDinoPass()
        {
            // Common.LogError("Active Dino Pass".Color("lime"));
            IsActiveVip = true;
        }

        // public void ClaimReward(EDinoPassChest type, int stageId)
        // {
        //     if (type == EDinoPassChest.Free)
        //         FreeRewardsClaimed.Add(stageId);
        //     else
        //         VipRewardsClaimed.Add(stageId);
        // }
    }
}