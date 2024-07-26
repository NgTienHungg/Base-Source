using System;
using System.Collections.Generic;
using Base;
using ViewPager.BoatPacking;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassDataSave : DataSaveBase
    {
        public bool IsActiveVip;
        public int CurrentStageId;
        public int CurrentPickaxes;
        public SerializableDateTime EndTime = new SerializableDateTime();
        public List<int> FreeRewardsClaimed = new List<int>();
        public List<int> VipRewardsClaimed = new List<int>();

        public DinoPassDataSave(string keySave) : base(keySave) { }

        public bool IsUnlockStage(int stageId)
        {
            return CurrentStageId > stageId;
        }

        public void Reset()
        {
            // Common.LogError("Reset Dino Pass".Color("yellow"));

            IsActiveVip = false;
            CurrentStageId = 0;
            CurrentPickaxes = 0;
            FreeRewardsClaimed.Clear();
            VipRewardsClaimed.Clear();

            // Reset end time to the beginning of the month
            var newEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).AddMonths(1);
            EndTime = new SerializableDateTime(newEndTime);

            // ProfileManager.Instance.LifeCount = BoatPackingConfig.MaxLife; // giam HP ve 3
            //
            // ProfileManager.Instance.SaveLocalUserData();
        }

        public void ActivateDinoPass()
        {
            IsActiveVip = true;
            // ProfileManager.Instance.LifeCount = BoatPackingConfig.MaxLifeDinoPass; // full HP luon
            // ProfileManager.Instance.SaveLocalUserData();
        }

        public void ClaimReward(EDinoPassChest type, int stageId)
        {
            if (type == EDinoPassChest.Free)
                FreeRewardsClaimed.Add(stageId);
            else
                VipRewardsClaimed.Add(stageId);

            // ProfileManager.Instance.SaveLocalUserData();
        }

        public bool IsRewardClaimed(EDinoPassChest type, int stageId)
        {
            bool isClaimed;
            if (type == EDinoPassChest.Free)
                isClaimed = FreeRewardsClaimed.Contains(stageId);
            else
                isClaimed = VipRewardsClaimed.Contains(stageId);

            return isClaimed;
        }

        public void AddPickaxe(int amount)
        {
            CurrentPickaxes += amount;
            // ProfileManager.Instance.SaveLocalUserData();
        }
    }
}